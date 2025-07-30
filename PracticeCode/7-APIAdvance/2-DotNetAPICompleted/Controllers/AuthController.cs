using System.Data;
using System.Security.Cryptography;
using Dapper;
using DotNetAPI.Data;
using DotNetAPI.Dtos;
using DotNetAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


namespace DotNetAPI.Controllers
{
    [Authorize] // Require authentication for this controller
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        // Fields
        private readonly DataContextDapper _dapper;
        private readonly AuthHelper _authHelper;

        // Constructor for AuthController
        public AuthController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
            _authHelper = new AuthHelper(config);
        }

        #region Endpoints
        // Register user and allow login method using post method endpoints
        [AllowAnonymous] // Allow anonymous access to this endpoint
        [HttpPost("Register")]
        public IActionResult Register(UserForRegistrationDto userForRegistration)
        {
            // Check if password and password confirm match
            if (userForRegistration.Password == userForRegistration.PasswordConfirm)
            {
                string sqlCheckUserExists = "SELECT Email FROM TutorialAppSchema.Auth WHERE Email = '" +
                    userForRegistration.Email + "'";

                IEnumerable<string> existingUsers = _dapper.LoadData<string>(sqlCheckUserExists);

                if (existingUsers.Count() == 0)
                {

                    UserForLoginDto userForSetPassword = new UserForLoginDto
                    {
                        Email = userForRegistration.Email,
                        Password = userForRegistration.Password
                    };

                    if (_authHelper.SetPassword(userForSetPassword))
                    {
                        string sqlAddUser = @"EXEC TutorialAppSchema.spUser_Upsert
                            @FirstName = '" + userForRegistration.FirstName +
                            "', @LastName = '" + userForRegistration.LastName +
                            "', @Email = '" + userForRegistration.Email +
                            "', @Gender = '" + userForRegistration.Gender +
                            "', @JobTitle  = '" + userForRegistration.JobTitle +
                            "', @Department = '" + userForRegistration.Department +
                            "', @Salary = '" + userForRegistration.Salary +
                            "', @Active = 1";

                        if (_dapper.ExecuteSql(sqlAddUser))
                        {
                            return Ok();
                        }
                        throw new Exception("Failed to add user");
                    }
                    throw new Exception("User registration failed");
                }
                throw new Exception("User already exists");
            }
            throw new Exception("Password and Password Confirm do not match");
        }
        #endregion

        #region Reset Password
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(UserForLoginDto userForSetPassword)
        {
            // Set the new password
            if (_authHelper.SetPassword(userForSetPassword))
            {
                return Ok();
            }
            throw new Exception("Failed to reset password");
        }
        #endregion

        #region Login and Token Endpoints
        [AllowAnonymous] 
        [HttpPost("Login")]
        public IActionResult Login(UserForLoginDto userForLogin)
        {
            string sqlForHashAndSalt = @"EXEC TutorialAppSchema.spLoginConfirmation_Get 
                @Email = @EmailParam";

            // Dynamic parameters
            DynamicParameters sqlParameters = new DynamicParameters();
            sqlParameters.Add("@EmailParam", userForLogin.Email, DbType.String);
            
            // Sql parameters
            // SqlParameter emailParameter = new SqlParameter("@EmailParam", SqlDbType.VarChar);
            // emailParameter.Value = userForLogin.Email;
            // sqlParameters.Add(emailParameter);

            UserForLoginConfirmationDto useForConfirmation = _dapper
                .LoadDataSingleWithParameters<UserForLoginConfirmationDto>(sqlForHashAndSalt, sqlParameters);

            byte[] passwordHash = _authHelper.GetPasswordHash(userForLogin.Password, useForConfirmation.PasswordSalt); 

            for (int index = 0; index < passwordHash.Length; index++)
            {
                if (passwordHash[index] != useForConfirmation.PasswordHash[index])
                {
                    return StatusCode(401, "Invalid password!");
                }
            }

            string userIdSql = @"
                SELECT UserId FROM TutorialAppSchema.Users WHERE Email = '"
                + userForLogin.Email + "'";

            int userId = _dapper.LoadDataSingle<int>(userIdSql);

            // Return the token based on user ID
            return Ok(new Dictionary<string, string>
            {
                { "token", _authHelper.CreateToken(userId)}
            });
        }

        [HttpGet("RefreshToken")]
        public IActionResult RefreshToken()
        {
            string userId = User.FindFirst("userId")?.Value + "";

            // Get user ID from the database
            string userIdSql = @"
                SELECT UserId FROM TutorialAppSchema.Users WHERE UserId = " + userId;

            int userIdFromDb = _dapper.LoadDataSingle<int>(userIdSql);

            return Ok(new Dictionary<string, string>
            {
                { "token", _authHelper.CreateToken(userIdFromDb)}
            });
        }
        #endregion

        #region Private Methods
        // Move to AuthHelper class

        #endregion
    }
}