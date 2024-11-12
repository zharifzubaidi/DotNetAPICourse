using System;
using System.Data; // For IDbConnection
using Microsoft.Data.SqlClient; // For setting sql connection
using dataPrac.models; // Importing other cs file using namespace
using Dapper; // For running SQL query in cs


namespace databasePractice{
    // Namespace practice code
    public class Program{
        public static void Main(string[] args) { 
            // Declaring SQL connection
            // Based on database name set in Azure Data Studio. Trust server immediately for test in local machine
            string connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;"; 
            // Create new connection to sql server
            IDbConnection dbConnection = new SqlConnection(connectionString);

            // Now can use SQL command
            string sqlCommand = "SELECT GETDATE()";
            // dbConnection.Query<DateTime>(sqlCommand); // Will return in array of row by default
            DateTime rightNow = dbConnection.QuerySingle<DateTime>(sqlCommand); // To get a single value
            // Write to console
            Console.WriteLine(rightNow);

            // // Declaring instance for model class
            // Computer myComputer = new Computer(){
            //     Motherboard = "Z690",
            //     HasWifi = true,
            //     HasLTE = true,
            //     ReleaseDate = DateTime.Now,
            //     Price = 943.87m,
            //     VideoCard = "RTX 2060"
            // };
            // // Alter value after declaring model
            // myComputer.HasWifi = false;
            // // Output attributes of model class
            // Console.WriteLine(myComputer.Motherboard);
            // Console.WriteLine(myComputer.HasWifi);
            // Console.WriteLine(myComputer.HasLTE);
            // Console.WriteLine(myComputer.ReleaseDate);
            // Console.WriteLine(myComputer.Price);
            // Console.WriteLine(myComputer.VideoCard);
        }
    }
}