using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ConfigExample.DatabaseLogic
{
    public class DataContextDapper
    {
        private IConfiguration _config;
        private string? _connectionString;
        // Constructor to initialize the DataContextDapper with a configuration
        public DataContextDapper(IConfiguration config)
        {
            // Set the connection string from the configuration
            // Two ways to get the connection string:
            // 1. Using the IConfiguration interface to get the connection string from appsettings.json
            // 2. Directly setting the connection string (not recommended for production)
            _config = config;   // Preferable way to get the connection string
            _connectionString = _config.GetConnectionString("DefaultConnection");

        }
        // Private field declaration

        // Method declaration using generic type so it can be used for any model
        // Encapsulates the logic for all database operations
        // Query array of data from the database
        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));   // From appsettings.json
            return dbConnection.Query<T>(sql);
        }
        // Query single data from the database
        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString); // From appsettings.json
            return dbConnection.QuerySingle<T>(sql);
        }
        // Execute a command that return true if affecting any rows
        public bool ExecuteSql(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString); // From appsettings.json
            return (dbConnection.Execute(sql) > 0);
        }
        // Execute a command that return the number of rows affected
        public int ExecuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection")); // From appsettings.json
            return dbConnection.Execute(sql);
        }

    }

    
}