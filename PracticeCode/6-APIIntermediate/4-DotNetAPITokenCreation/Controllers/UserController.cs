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
public class UserController : ControllerBase
{
    DataContextDapper _dapper;

    public UserController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    #region User Endpoints
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

    [HttpGet("Get")]

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
    #endregion

    #region User Salary Endpoints
    [HttpGet("GetUserSalary/{userId}")]
    public IEnumerable<UserSalary> GetUserSalary(int userId)
    {
        return _dapper.LoadData<UserSalary>(@"
            SELECT UserSalary.UserId
                    , UserSalary.Salary
                    , UserSalary.AverageSalary
            FROM TutorialAppSchema.UserSalary
                WHERE UserId = " + userId.ToString());
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

    #endregion

    #region User Job Info Endpoints

    [HttpGet("GetUserJobInfo/{userId}")]
    public IEnumerable<UserJobInfo> GetUserJobInfo(int userId)
    {
        return _dapper.LoadData<UserJobInfo>(@"
            SELECT UserJobInfo.UserId
                    , UserJobInfo.JobTitle
                    , UserJobInfo.Department
            FROM TutorialAppSchema.UserJobInfo
                WHERE UserId = " + userId.ToString());
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

    #endregion
}

