// User Controller

using DotNetAPI.Data;
using DotNetAPI.UserModels;
using Microsoft.AspNetCore.Mvc;
using DotNetAPI.UserDtos;
using AutoMapper;
using DotNetAPI.UserSalaryModels;
using DotNetAPI.UserJobInfoModels;

namespace DotNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{
    // Remove direct access to entity framework context
    // and use repository pattern instead
    //DataContextEF _entityFramework;
    IUserRepository _userRepository;
    IMapper _mapper;

    public UserEFController(IConfiguration config, IUserRepository userRepository)
    {
        // Map private field to the injected parameter

        //_entityFramework = new DataContextEF(config); // removed direct access to EF context

        _userRepository = userRepository;

        ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });

        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserToAddDto, User>();
            cfg.CreateMap<UserSalary, UserSalary>().ReverseMap();
            cfg.CreateMap<UserJobInfo, UserJobInfo>().ReverseMap();
        }, loggerFactory));
    }

    #region User Endpoints
    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = _userRepository.GetUsers();
        return users;
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        // User? user = _entityFramework.Users
        //     .Where(u => u.UserId == userId)
        //     .FirstOrDefault<User>();
        // if (user != null)
        // {
        //     return user;
        // }
        // throw new Exception("Failed to Get Single User");

        return _userRepository.GetSingleUser(userId); // Result of using repository pattern
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        User? userDb = _userRepository.GetSingleUser(user.UserId);

        if (userDb != null)
        {
            userDb.Active = user.Active;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;

            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Update User");
        }
        throw new Exception("Failed to Get User");
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {

        User userDb = _mapper.Map<User>(user);

        _userRepository.AddEntity<User>(userDb);

        if (_userRepository.SaveChanges())
        {
            return Ok();
        }
        throw new Exception("Failed to Add User");

    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        User? userDb = _userRepository.GetSingleUser(userId);

        if (userDb != null)
        {
            _userRepository.RemoveEntity<User>(userDb);

            if (_userRepository.SaveChanges())
            {
                return Ok();
            }

            throw new Exception("Failed to Delete User");
        }
        throw new Exception("Failed to Get User");
    }

    #endregion

    #region User Salary Endpoints
    [HttpGet("GetUserSalary/{userId}")]
    public UserSalary GetUserSalariesEF(int userId)
    {
        return _userRepository.GetSingleUserSalary(userId);
    }

    [HttpPost("AddUserSalary")]
    public IActionResult AddUserSalaryEF(UserSalary userForAdd)
    {
        _userRepository.AddEntity<UserSalary>(userForAdd);
        if (_userRepository.SaveChanges())
        {
            return Ok();
        }

        throw new Exception("Failed to Add User Salary");
    }

    [HttpPut("EditUserSalary")]
    public IActionResult UpdateUserSalaryEF(UserSalary userForUpdate)
    {

        UserSalary? userToUpdate = _userRepository.GetSingleUserSalary(userForUpdate.UserId);

        if (userToUpdate != null)
        {
            _mapper.Map(userForUpdate, userToUpdate);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Update User Salary");
        }
        throw new Exception("Failed to Get User Salary");
    }

    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalaryEF(int userId)
    {
        UserSalary? userToDelete = _userRepository.GetSingleUserSalary(userId);

        if (userToDelete != null)
        {
            _userRepository.RemoveEntity<UserSalary>(userToDelete);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Delete User Salary");
        }
        throw new Exception("Failed to Get User Salary");
    }

    #endregion

    #region User Job Info Endpoints

    [HttpGet("GetUserJobInfo/{userId}")]
    public UserJobInfo GetUserJobInfoEF(int userId)
    {
        return _userRepository.GetSingleUserJobInfo(userId);
    }

    [HttpPost("AddUserJobInfo")]
    public IActionResult AddUserJobInfoEF(UserJobInfo userForAdd)
    {
        _userRepository.AddEntity<UserJobInfo>(userForAdd);
        if (_userRepository.SaveChanges())
        {
            return Ok();
        }
        throw new Exception("Failed to Add User Job Info");
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult UpdateUserJobInfoEF(UserJobInfo userForUpdate)
    {
        UserJobInfo? userToUpdate = _userRepository.GetSingleUserJobInfo(userForUpdate.UserId);

        if (userToUpdate != null)
        {
            _mapper.Map(userForUpdate, userToUpdate);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Update User Job Info");
        }
        throw new Exception("Failed to Get User Job Info");
    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfoEF(int userId)
    {
        UserJobInfo? userToDelete = _userRepository.GetSingleUserJobInfo(userId);

        if (userToDelete != null)
        {
            _userRepository.RemoveEntity<UserJobInfo>(userToDelete);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Delete User Job Info");
        }
        throw new Exception("Failed to Get User Job Info");
    }

    #endregion
}