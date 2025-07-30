using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using DotNetAPI.Data;
using DotNetAPI.Dtos;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace DotNetAPI.Helpers
{
    public class AuthHelper
    {
        private readonly IConfiguration _config;
        private readonly DataContextDapper _dapper;

        // Constructor for AuthController
        public AuthHelper(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
            _config = config;
        }
        public byte[] GetPasswordHash(string password, byte[] passwordSalt)
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

        public string CreateToken(int userId)
        {
            // Token creation logic would go here
            // This is a placeholder for the actual token generation logic
            // Claims are used to store user information in the token
            Claim[] claims = new Claim[]
            {
                new Claim("userId", userId.ToString()),
            };

            string? tokenKeyString = _config.GetSection("AppSettings:TokenKey").Value;

            // Symmetric security key generation for the token
            SymmetricSecurityKey tokenKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    tokenKeyString != null ? tokenKeyString : ""
                )
            );

            // Signing credentials for the token
            SigningCredentials Credentials = new SigningCredentials(
                tokenKey,
                SecurityAlgorithms.HmacSha512Signature
            );

            // Token descriptor creation combines claims, key and signing credentials
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = Credentials,
                Expires = DateTime.Now.AddDays(7) // Token expiration time

            };

            // To turn descriptor into a token that can be passed back to the user
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);    // Token created
            return tokenHandler.WriteToken(token);                              // Token written to string
        }

        public bool SetPassword(UserForLoginDto userForSetPassword)
        {
            byte[] passwordSalt = new byte[128 / 8];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetNonZeroBytes(passwordSalt);   
            }

            byte[] passwordHash = GetPasswordHash(userForSetPassword.Password, passwordSalt);

            string sqlAddAuth = @"EXEC TutorialAppSchema.spRegistration_Upsert
                    @Email = @EmailParam,
                    @PasswordHash = @PasswordHashParam,
                    @PasswordSalt = @PasswordSaltParam";

            #region SQL Parameters
            // List<SqlParameter> sqlParameters = new List<SqlParameter>();

            // SqlParameter emailParameter = new SqlParameter("@EmailParam", SqlDbType.VarChar);
            // emailParameter.Value = userForSetPassword.Email;
            // sqlParameters.Add(emailParameter);

            // SqlParameter passwordSaltParameter = new SqlParameter("@PasswordSaltParam", SqlDbType.VarBinary);
            // passwordSaltParameter.Value = passwordSalt;
            // sqlParameters.Add(passwordSaltParameter);

            // SqlParameter passwordHashParameter = new SqlParameter("@PasswordHashParam", SqlDbType.VarBinary);
            // passwordHashParameter.Value = passwordHash;
            // sqlParameters.Add(passwordHashParameter);
            #endregion
            
            DynamicParameters sqlParameters = new DynamicParameters();

            sqlParameters.Add("@EmailParam", userForSetPassword.Email, DbType.String);
            sqlParameters.Add("@PasswordSaltParam", passwordSalt, DbType.Binary);
            sqlParameters.Add("@PasswordHashParam", passwordHash, DbType.Binary);

            // Execute the SQL command to add the user
            return _dapper.ExecuteSqlWithParameters(sqlAddAuth, sqlParameters);
        }
    }
}