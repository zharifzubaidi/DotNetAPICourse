// User Controller

using System.Data;
using Dapper;
using DotNetAPI.Data;
using DotNetAPI.UserModels;
using Microsoft.AspNetCore.Mvc;

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
        string sql = @"EXEC TutorialAppSchema.spUsers_Get";
        string stringParameters = "";
        DynamicParameters sqlParameters = new DynamicParameters();

        if (userId != 0)
        {
            stringParameters += ", @UserId = @UserIdParam";
            sqlParameters.Add("@UserIdParam", userId, DbType.Int32);
        }
        
        if (isActive)
        {
            stringParameters += ", @Active = @ActiveParam";
            sqlParameters.Add("@ActiveParam", isActive, DbType.Boolean);
        }

        if (stringParameters.Length > 0)
        {
            sql += stringParameters.Substring(1);
        }

        IEnumerable<UserComplete> users = _dapper.LoadDataWithParameters<UserComplete>(sql, sqlParameters);

        return users;
    }

    [HttpPut("UpsertUser")]
    public IActionResult UpsertUser(UserComplete user)
    {
        string sql = @"EXEC TutorialAppSchema.spUser_Upsert
            @FirstName = @FirstNameParam,
            @LastName = @LastNameParam,
            @Email = @EmailParam,
            @Gender = @GenderParam,
            @JobTitle  = @JobTitleParam,
            @Department = @DepartmentParam,
            @Salary = @SalaryParam,
            @Active = @ActiveParam,
            @UserId = @UserIdParam";

        DynamicParameters sqlParameters = new DynamicParameters();

        sqlParameters.Add("@FirstNameParam", user.FirstName, DbType.String);
        sqlParameters.Add("@LastNameParam", user.LastName, DbType.String);
        sqlParameters.Add("@EmailParam", user.Email, DbType.String);
        sqlParameters.Add("@GenderParam", user.Gender, DbType.String);
        sqlParameters.Add("@JobTitleParam", user.JobTitle, DbType.String);
        sqlParameters.Add("@DepartmentParam", user.Department, DbType.String);
        sqlParameters.Add("@SalaryParam", user.Salary, DbType.Decimal);
        sqlParameters.Add("@ActiveParam", user.Active, DbType.Boolean);
        sqlParameters.Add("@UserIdParam", user.UserId, DbType.Int32);

        if (_dapper.ExecuteSqlWithParameters(sql, sqlParameters))
        {
            return Ok();
        }

        throw new Exception("Failed to update user");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"EXEC TutorialAppSchema.spUser_Delete
            @UserId = @UserIdParam";
        
        DynamicParameters sqlParameters = new DynamicParameters();

        sqlParameters.Add("@UserIdParam", userId, DbType.Int32);

        if (_dapper.ExecuteSqlWithParameters(sql, sqlParameters))
        {
            return Ok();
        }
        throw new Exception("Failed to delete user");
    }

}

