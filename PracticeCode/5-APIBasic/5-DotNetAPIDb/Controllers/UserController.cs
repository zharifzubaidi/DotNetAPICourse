// User Controller

using DotNetAPI.Data;
using Microsoft.AspNetCore.Mvc;

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

    // API endpoint for load data from database
    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }

    // API endpoint to get users
    [HttpGet("GetUsers/{testValue}")]
    public string[] GetUsers(string testValue)
    {
        string[] responseArray = new string[] {
            "Test 1",
            "Test 2",
            testValue
        };
        return responseArray;
    }
    
    // --------------------------------------------API Endpoints--------------------------------------------//
}

