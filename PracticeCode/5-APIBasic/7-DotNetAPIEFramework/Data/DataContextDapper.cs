using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DotNetAPI.Data
{
    class DataContextDapper
    {
        // Class member
        private readonly IConfiguration _config;

        public DataContextDapper(IConfiguration config)     // Method to inject configuration from appsettings.json
        {
            // Constructor to initialize the configuration
            _config = config;   // load the injected appsettings.json data into private field _config
        }

        // Retrieve multiple rows of data from the database
        public IEnumerable<T> LoadData<T>(string sql)
        {
            // Get the connection string from appsettings.json through private field config
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            // Use Dapper to execute the query and return the results
            return dbConnection.Query<T>(sql);
        }

        // Retrieve a single row of data from the database
        public T LoadDataSingle<T>(string sql)
        {
            // Get the connection string from appsettings.json through private field config
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            // Use Dapper to execute the query and return the results
            return dbConnection.QuerySingle<T>(sql);
        }

        // Execute SQL commands
        public bool ExecuteSql(string sql)
        {
            // Get the connection string from appsettings.json through private field config
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            // Use Dapper to execute the command and return the result
            return dbConnection.Execute(sql) > 0;
        }

        // Execute SQL commands and return the number of affected rows
        public int ExecuteSqlWithAffectedRows(string sql)
        {
            // Get the connection string from appsettings.json through private field config
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            // Use Dapper to execute the command and return the number of affected rows
            return dbConnection.Execute(sql);
        }
    }
}