// User Controller

using Microsoft.AspNetCore.Mvc;

namespace DotNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    // Constructor
    public UserController()
    {
        // Initialization code can go here if needed
    }

    //[HttpGet("test")]                 // Implicit route
    [HttpGet("GetUsers/{testValue}")]       // Explicit route with parameter. Suitable for API that requires a value
    // public IActionResult Test()      // API response
    public string[] GetUsers(string testValue)
    {
        string[] responseArray = new string[] {
            "Test 1",
            "Test 2",
            testValue
        };
        return responseArray;
    }
}

