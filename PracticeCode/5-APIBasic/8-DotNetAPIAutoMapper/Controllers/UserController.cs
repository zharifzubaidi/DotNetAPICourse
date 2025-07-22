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
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }

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
        WHERE UserId = " + userId.ToString(); 

        User user = _dapper.LoadDataSingle<User>(sql);
        return user;
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
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
        Console.WriteLine(sql); 
        if (_dapper.ExecuteSql(sql))
        {
            return Ok(); 
        }

        throw new Exception("Failed to update user");
    }

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
        Console.WriteLine(sql); 
        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }
        throw new Exception("Failed to add user");
    }

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
        Console.WriteLine(sql);
        if (_dapper.ExecuteSql(sql))
        {
            return Ok(); 
        }
        throw new Exception("Failed to add user DTO");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"
        DELETE FROM TutorialAppSchema.Users
        WHERE UserId = " + userId.ToString();

        Console.WriteLine(sql);
        if (_dapper.ExecuteSql(sql))
        {
            return Ok(); // Returns a 200 OK response
        }
        throw new Exception("Failed to delete user");
    }
}

