using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DapperExample.DatabaseLogic
{
    public class DataContextDapper
    {
        // Private field declaration
        private string _connectionString = "Server=CSD-ZHARIF\\SQLEXPRESS;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";

        // Method declaration using generic type so it can be used for any model
        // Encapsulates the logic for all database operations
        // Query array of data from the database
        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Query<T>(sql);
        }
        // Query single data from the database
        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.QuerySingle<T>(sql);
        }
        // Execute a command that return true if affecting any rows
        public bool ExecuteSql(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return (dbConnection.Execute(sql) > 0);
        }
        // Execute a command that return the number of rows affected
        public int ExecuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql);
        }

    }
}