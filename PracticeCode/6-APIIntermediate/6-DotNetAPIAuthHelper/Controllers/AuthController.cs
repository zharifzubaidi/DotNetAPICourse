using System.Security.Cryptography;
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

                // Check if user already exists in the database
                IEnumerable<string> existingUsers = _dapper.LoadData<string>(sqlCheckUserExists);

                // If not exists, then add the user
                if (existingUsers.Count() == 0)
                {
                    // Password salt setup
                    byte[] passwordSalt = new byte[128 / 8];

                    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                    {
                        rng.GetNonZeroBytes(passwordSalt);      // populate passwordSalt with random bytes
                    }

                    /* Move to a separate method
                    // Increasing the password size
                    string passwordSaltPlusString = _config.GetSection("AppSettings:PasswordKey").Value +
                        Convert.ToBase64String(passwordSalt);

                    // Password hash setup
                    byte[] passwordHash = KeyDerivation.Pbkdf2(
                        password: userForRegistration.Password,
                        salt: Encoding.ASCII.GetBytes(passwordSaltPlusString),
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 100000,
                        numBytesRequested: 256 / 8
                    );
                    */
                    // Get the password hash using the password and salt based on the user input
                    byte[] passwordHash = _authHelper.GetPasswordHash(userForRegistration.Password, passwordSalt);

                    string sqlAddAuth = "INSERT INTO TutorialAppSchema.Auth (Email, PasswordHash, PasswordSalt) "
                        + "VALUES ('" + userForRegistration.Email
                        + "', @PasswordHash, @PasswordSalt)"; // @ means parameterized query or variable

                    // Store password hash and salt in the database via SQL parameters
                    // Using SqlParameter to prevent SQL injection attacks
                    List<SqlParameter> sqlParameters = new List<SqlParameter>();

                    SqlParameter passwordSaltParameter = new SqlParameter("@PasswordSalt", System.Data.SqlDbType.VarBinary);
                    passwordSaltParameter.Value = passwordSalt;

                    SqlParameter passwordHashParameter = new SqlParameter("@PasswordHash", System.Data.SqlDbType.VarBinary);
                    passwordHashParameter.Value = passwordHash;

                    sqlParameters.Add(passwordSaltParameter);
                    sqlParameters.Add(passwordHashParameter);

                    // Execute the SQL command to add the user
                    if (_dapper.ExecuteSqlWithParameters(sqlAddAuth, sqlParameters))
                    {
                        string sqlAddUser = @"
                        INSERT INTO TutorialAppSchema.Users(
                            [FirstName],
                            [LastName],
                            [Email],
                            [Gender],
                            [Active]
                        ) VALUES( " +
                            "'" + userForRegistration.FirstName +
                            "', '" + userForRegistration.LastName +
                            "', '" + userForRegistration.Email +
                            "', '" + userForRegistration.Gender +
                            "', 1)";

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

        [AllowAnonymous] 
        [HttpPost("Login")]
        public IActionResult Login(UserForLoginDto userForLogin)
        {
            string sqlForHashAndSalt = "SELECT PasswordHash, PasswordSalt FROM TutorialAppSchema.Auth WHERE Email = '"
                + userForLogin.Email + "'";

            // Load password hash and salt from the database
            UserForLoginConfirmationDto useForConfirmation = _dapper
                .LoadDataSingle<UserForLoginConfirmationDto>(sqlForHashAndSalt);

            // Load password hash and salt from the user input
            byte[] passwordHash = _authHelper.GetPasswordHash(userForLogin.Password, useForConfirmation.PasswordSalt);  // Get the password hash: passwordHash = userForConfirmation.Password

            // if(passwordHash == userForConfirmation.PasswordHash) // Won't work because it will only compare references (pointer comparison) not value
            // Compare each byte in the password hash to confirm the password is correct
            for (int index = 0; index < passwordHash.Length; index++)
            {
                if (passwordHash[index] != useForConfirmation.PasswordHash[index])
                {
                    //throw new Exception("Invalid password");
                    return StatusCode(401, "Invalid password!");
                }
            }

            //return Ok();

            // Retrieve user ID from the database using the email
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