using System;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using Dapper;
using Microsoft.Data.SqlClient;

namespace database.data
{
    // Dapper class
    public class DataContextDapper
    {
        // Private field in class
        // Connecting to SQL server
        string _connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";

        // Method in class
        public IEnumerable<T> LoadData<T>(string sql)
        {
            
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Query<T>(sql);

        }

        public T LoadDataSingle<T>(string sql)
        {

            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.QuerySingle<T>(sql);

        }

        public bool ExecuteSql(string sql)
        {
            
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return (dbConnection.Execute(sql) > 0);
        
        }

        public int ExecuteSqlWRows(string sql)
        {

            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql);

        }
    }
}