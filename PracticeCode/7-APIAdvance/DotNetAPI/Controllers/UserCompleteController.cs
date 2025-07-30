// User Controller

using DotNetAPI.Data;
using DotNetAPI.UserModels;
using Microsoft.AspNetCore.Mvc;
using DotNetAPI.UserDtos;
using DotNetAPI.UserSalaryModels;
using DotNetAPI.UserJobInfoModels;

namespace DotNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserCompleteController : ControllerBase
{
    DataContextDapper _dapper;

    public UserCompleteController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetUsers/{userId}/{isActive}")]
    public IEnumerable<UserComplete> GetUsers(int userId, bool isActive)
    {
        string sql = @"EXEC TutorialAppSchema.spUsers_Get"; // Will get all users if no userId is provided
        string param = "";

        if (userId != 0)
        {   // If userId is provided, append it to the SQL query
            param += ", @UserId =" + userId.ToString(); // Run stored procedure to get complete user data
        }
        
        if (isActive)
        { 
            param += ", @Active =" + isActive.ToString(); // Run stored procedure to get complete user data
        }

        sql += param.Substring(1); // Remove the first comma

        IEnumerable<UserComplete> users = _dapper.LoadData<UserComplete>(sql);

        return users;
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


    [HttpPost("AddUserSalary")]
    public IActionResult AddUserSalary(UserSalary userForAdd)
    {
        string sql = @"
            INSERT INTO TutorialAppSchema.UserSalary(
                [UserId],
                [Salary]
            ) VALUES (" + userForAdd.UserId +
                ", " + userForAdd.Salary +
            ")";

        Console.WriteLine(sql);

        if (_dapper.ExecuteSqlWithAffectedRows(sql) > 0)
        {
            return Ok(userForAdd);
        }

        throw new Exception("Failed to Add User Salary");
    }

    [HttpPut("EditUserSalary")]
    public IActionResult UpdateUserSalary(UserSalary userSalaryForUpdate)
    {
        string sql = @"
            UPDATE TutorialAppSchema.UserSalary
            SET Salary = " + userSalaryForUpdate.Salary +
            " WHERE UserId = " + userSalaryForUpdate.UserId;

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok(userSalaryForUpdate);
        }

        throw new Exception("Failed to Update User Salary");
    }

    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        string sql = "DELETE FROM TutorialAppSchema.UserSalary WHERE UserId = " + userId;//.ToString();

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Delete User Salary");
    }


    [HttpPost("AddUserJobInfo")]
    public IActionResult AddUserJobInfo(UserJobInfo userJobInfoForAdd)
    {
        string sql = @"
            INSERT INTO TutorialAppSchema.UserJobInfo(
                [UserId],
                [JobTitle],
                [Department]
            ) VALUES (" + userJobInfoForAdd.UserId +
                ", '" + userJobInfoForAdd.JobTitle +
                "', '" + userJobInfoForAdd.Department +
            "')";

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok(userJobInfoForAdd);
        }

        throw new Exception("Failed to Add User Job Info");
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult UpdateUserJobInfo(UserJobInfo userJobInfoForUpdate)
    {
        string sql = @"
            UPDATE TutorialAppSchema.UserJobInfo
            SET JobTitle = '" + userJobInfoForUpdate.JobTitle +
            "', Department = '" + userJobInfoForUpdate.Department +
            "' WHERE UserId = " + userJobInfoForUpdate.UserId;

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok(userJobInfoForUpdate);
        }

        throw new Exception("Failed to Update User Job Info");
    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        string sql = @"
            DELETE FROM TutorialAppSchema.UserJobInfo
                WHERE UserId = " + userId;

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Delete User Job Info");
    }

}

