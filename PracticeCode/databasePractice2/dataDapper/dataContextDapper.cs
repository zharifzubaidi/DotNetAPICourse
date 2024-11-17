using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace dataPrac.dapper{
    public class DataContextDapper{
        // Declaring SQL connection
        // Based on database name set in Azure Data Studio. Trust server immediately for test in local machine
        private string _connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;"; 
        // Load data in new row of the SQL data as a method. Can load dynamic model (format).
        // Need to define type/model and then sql statement
        public IEnumerable<T> LoadData<T>(string sql){
            // Create new connection to sql server
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Query<T>(sql);
        }

        ExecuteSql    
            
    }
}
