// User Controller

using DotNetAPI.Data;
using DotNetAPI.UserModels;
using Microsoft.AspNetCore.Mvc;
using DotNetAPI.UserDtos;

namespace DotNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    DataContextDapper _dapper;

    public UserController(IConfiguration config)
    {
        //Console.WriteLine(config.GetConnectionString("DefaultConnection"));
        _dapper = new DataContextDapper(config);
    }

    // --------------------------------------------API Endpoints--------------------------------------------//
    // GET - to retrieve data
    // POST - to create new data
    // PUT - to update existing data
    // DELETE - to delete existing data

    // API endpoint for load data from database
    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }

    // -----------------------------API GET endpoint----------------------------- //
    // API endpoint to get users
    // Endpoint auto convert from PascalCase to camelCase
    // Tag name is similar to method name
    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        string sql = @"
        SELECT [UserId],
            [FirstName],
            [LastName],
            [Email],
            [Gender],
            [Active] 
        FROM TutorialAppSchema.Users";

        IEnumerable<User> users = _dapper.LoadData<User>(sql);

        return users;
    }

    // API endpoint to get a single user by UserId
    // API endpoint URL with URL parameter
    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        string sql = @"
        SELECT [UserId],
            [FirstName],
            [LastName],
            [Email],
            [Gender],
            [Active] 
        FROM TutorialAppSchema.Users
        WHERE UserId = " + userId.ToString();  // Append the int input in a string format

        User user = _dapper.LoadDataSingle<User>(sql);
        return user;
    }
    // -----------------------------API GET endpoint----------------------------- //

    // -----------------------------API PUT endpoint----------------------------- //
    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        // For boolean, we can use inline ternary operator to convert boolean 
        // to SQL boolean bit value (1 for true, 0 for false)
        // Alternative -> @"', [Active] = " + (user.Active ? 1: 0)
        // But normally, Automapper will automap the C# boolean to SQL bit value
        string sql = @"
        UPDATE TutorialAppSchema.Users
            SET [FirstName] = '" + user.FirstName +
                @"', 
                [LastName] = '" + user.LastName +
                @"', 
                [Email] = '" + user.Email +
                @"', 
                [Gender] = '" + user.Gender +
                @"', 
                [Active] = '" + user.Active + @"'
            WHERE [UserId] = " + user.UserId;
        Console.WriteLine(sql); // For debugging purposes, to see the generated SQL query
        if (_dapper.ExecuteSql(sql))
        {
            return Ok(); // Returns a 200 OK response
        }

        throw new Exception("Failed to update user");
    }
    // -----------------------------API PUT endpoint----------------------------- //

    // -----------------------------API POST endpoint---------------------------- //
    // Model of complete mapping to database fields
    // This is not recommended for insert query as it will include the primary key
    [HttpPost("AddUser")]
    public IActionResult AddUser(User user)
    {
        string sql = @"
        INSERT INTO TutorialAppSchema.Users(
            [FirstName],
            [LastName],
            [Email],
            [Gender],
            [Active]
        ) VALUES( " +
            "'" + user.FirstName +
            "', '" + user.LastName +
            "', '" + user.Email +
            "', '" + user.Gender +
            "', '" + user.Active +
            "')";
        Console.WriteLine(sql); // For debugging purposes, to see the generated SQL query
        if (_dapper.ExecuteSql(sql))
        {
            return Ok(); // Returns a 200 OK response
        }
        throw new Exception("Failed to add user");
    }

    // DTO application to exclude the primary key by creating a new DTO model
    [HttpPost("AddUserDTO")]
    public IActionResult AddUserDTO(UserToAddDto user)
    {
        string sql = @"
        INSERT INTO TutorialAppSchema.Users(
            [FirstName],
            [LastName],
            [Email],
            [Gender],
            [Active]
        ) VALUES( " +
            "'" + user.FirstName +
            "', '" + user.LastName +
            "', '" + user.Email +
            "', '" + user.Gender +
            "', '" + user.Active +
            "')";
        Console.WriteLine(sql); // For debugging purposes, to see the generated SQL query
        if (_dapper.ExecuteSql(sql))
        {
            return Ok(); // Returns a 200 OK response
        }
        throw new Exception("Failed to add user DTO");
    }
    // -----------------------------API POST endpoint---------------------------- //

    // -----------------------------API DELETE endpoint---------------------------- //
    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"
        DELETE FROM TutorialAppSchema.Users
        WHERE UserId = " + userId.ToString();

        Console.WriteLine(sql); // For debugging purposes, to see the generated SQL query
        if (_dapper.ExecuteSql(sql))
        {
            return Ok(); // Returns a 200 OK response
        }
        throw new Exception("Failed to delete user");
    }

    // -----------------------------API DELETE endpoint---------------------------- //


    // --------------------------------------------API Endpoints--------------------------------------------//
}

