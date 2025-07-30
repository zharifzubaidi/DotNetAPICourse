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

        if (param.Length > 0)
        {
            // If there are parameters, append them to the SQL query
            sql += param.Substring(1); // Remove the first comma
        }

        IEnumerable<UserComplete> users = _dapper.LoadData<UserComplete>(sql);

        return users;
    }

    [HttpPut("UpsertUser")]
    public IActionResult UpsertUser(UserComplete user)
    {
        string sql = @"EXEC TutorialAppSchema.spUser_Upsert
            @FirstName = '" + user.FirstName +
            "', @LastName = '" + user.LastName +
            "', @Email = '" + user.Email +
            "', @Gender = '" + user.Gender +
            "', @JobTitle  = '" + user.JobTitle +
            "', @Department = '" + user.Department +
            "', @Salary = '" + user.Salary +
            "', @Active = '" + user.Active +
            "', @UserId = " + user.UserId;

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to update user");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"EXEC TutorialAppSchema.spUser_Delete
            @UserId = " + userId.ToString();

        if (_dapper.ExecuteSql(sql))
        {
            return Ok(); // Returns a 200 OK response
        }
        throw new Exception("Failed to delete user");
    }

}

