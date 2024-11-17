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
            // Console.WriteLine(rightNow.ToString());

            // Declaring instance for model class
            Computer myComputer = new Computer(){
                Motherboard = "Z690",
                HasWifi = true,
                HasLTE = true,
                ReleaseDate = DateTime.Now,
                Price = 943.87m,
                VideoCard = "RTX 2060"
            };

            // Inserting data in a new row of a sql database
            // Creating a sql statement to generate a table. Declare variable name and followed by values.
            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES ('" + myComputer.Motherboard
                + "','" + myComputer.HasWifi
                + "','" + myComputer.HasLTE
                + "','" + myComputer.ReleaseDate.ToString("yyyy-MM-dd")
                + "','" + myComputer.Price
                + "','" + myComputer.VideoCard
            + "')";
            // Console.WriteLine(sql);

            // Send the sql statement via dapper library
            int result = dbConnection.Execute(sql);
            // Check how many rows were change after sending the sql statement
            // Console.WriteLine(result);

            // Reading all data from sql database
            string sqlSelect = @"
            SELECT
                Computer.Motherboard,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard
            FROM TutorialAppSchema.Computer";
            // Query (not single) will return in IEnumerable datatype. Need to casting IEnumerable to list by using .ToList() at the end.
            IEnumerable<Computer> computers = dbConnection.Query<Computer>(sqlSelect);
            // Loop in IEnumerable of data return from query and arrange it using foreach loop
            Console.WriteLine("'Motherboard','HasWifi','HasLTE','ReleaseDate','Price','VideoCard'");
            foreach(Computer singleComputer in computers){
                Console.WriteLine("'" + singleComputer.Motherboard
                + "','" + singleComputer.HasWifi
                + "','" + singleComputer.HasLTE
                + "','" + singleComputer.ReleaseDate
                + "','" + singleComputer.Price
                + "','" + singleComputer.VideoCard
                + "'");
            }
        }
    }
}