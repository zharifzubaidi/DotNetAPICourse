using System.Security.Cryptography;
using System.Text;
using DotNetAPI.Data;
using DotNetAPI.Dtos;
using DotNetAPI.UserModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DotNetAPI.Controllers
{
    public class AuthController : ControllerBase
    {
        // Fields
        private readonly DataContextDapper _dapper;
        private readonly IConfiguration _config;


        // Constructor for AuthController
        public AuthController(IConfiguration config)
        {

            _dapper = new DataContextDapper(config);
            _config = config;
        }

        #region Endpoints
        // Register user and allow login method using post method endpoints
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
                    byte[] passwordHash = GetPasswordHash(userForRegistration.Password, passwordSalt);

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
                        return Ok();
                    }
                    throw new Exception("User registration failed");
                }
                throw new Exception("User already exists");
            }
            throw new Exception("Password and Password Confirm do not match");
        }

        [HttpPost("Login")]
        public IActionResult Login(UserForLoginDto userForLogin)
        {
            string sqlForHashAndSalt = "SELECT PasswordHash, PasswordSalt FROM TutorialAppSchema.Auth WHERE Email = '"
                + userForLogin.Email + "'";

            // Load password hash and salt from the database
            UserForLoginConfirmationDto useForConfirmation = _dapper
                .LoadDataSingle<UserForLoginConfirmationDto>(sqlForHashAndSalt);

            // Load password hash and salt from the user input
            byte[] passwordHash = GetPasswordHash(userForLogin.Password, useForConfirmation.PasswordSalt);  // Get the password hash: passwordHash = userForConfirmation.Password

            // if(passwordHash == userForConfirmation.PasswordHash) // Won't work because it will only compare references (pointer comparison) not value
            // Compare each byte in the password hash to confirm the password is correct
            for(int index = 0; index < passwordHash.Length; index++)
            {
                if (passwordHash[index] != useForConfirmation.PasswordHash[index])
                {
                    //throw new Exception("Invalid password");
                    return StatusCode(401, "Invalid password!");
                }
            }

            return Ok();
        }
        #endregion

        #region Private Methods
        private byte[] GetPasswordHash(string password, byte[] passwordSalt)
        {
            string passwordSaltPlusString = _config.GetSection("AppSettings:PasswordKey").Value +
                            Convert.ToBase64String(passwordSalt);

            // Password hash return
            return KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(passwordSaltPlusString),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
            );
        }
        #endregion
    }
}