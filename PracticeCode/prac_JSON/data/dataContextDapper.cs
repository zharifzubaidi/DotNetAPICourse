using System;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace prac_JSON.data
{
    // Dapper class
    public class DataContextDapper
    {
        // Private field in class
        private IConfiguration _config;

        // Class constructor that allow DataContextDapper class to receive an object as argument
        public DataContextDapper(IConfiguration config)
        {
            _config = config;
        }
        
        // Private field in class
        // Connecting to SQL server
        // string _connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";

        // Method in class
        public IEnumerable<T> LoadData<T>(string sql)
        {
            // Instead of using string for SQL connection, we now use the value from appsettings.json via _config
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Query<T>(sql);

        }

        public T LoadDataSingle<T>(string sql)
        {

            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.QuerySingle<T>(sql);

        }

        public bool ExecuteSql(string sql)
        {
            
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return (dbConnection.Execute(sql) > 0);
        
        }

        public int ExecuteSqlWRows(string sql)
        {

            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql);

        }
    }
}