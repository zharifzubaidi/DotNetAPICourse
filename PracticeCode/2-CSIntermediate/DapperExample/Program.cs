using System.Data;
using System.Diagnostics.Contracts;
using Dapper;
using DapperExample.Models; // namespace <project name>.<folder name for the model or class name>
using DapperExample.DatabaseLogic;
using Microsoft.Data.SqlClient;

namespace DapperExample
{
    // Main Class serves as the entry point for the application
    internal class Program
    {
        static void Main(string[] args)
        {
            // Move to Database Logic folder
            // 01 Connection to database using Dapper tutorial
            /*
            Create connection to the database using Dapper
            string connectionString = "Server=CSD-ZHARIF\\SQLEXPRESS;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";
            IDbConnection dbConnection = new SqlConnection(connectionString);
            */

            // 02 Retrieve data from the database using query method
            /*
            string sqlCommand = "SELECT GETDATE()"; // Example SQL command to get the current date and time
            // Dapper call query method to execute the SQL command and retrieve the current date
            DateTime currentDate = dbConnection.QuerySingle<DateTime>(sqlCommand);
            Console.WriteLine($"Current Date and Time: {currentDate}");
            */

            // 05 Using Dapper with class models
            DataContextDapper dapper = new DataContextDapper();
            DateTime rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
            Console.WriteLine($"Current Date and Time: {rightNow}");

            // 03 Manipulate data in database using execute method
            // Create an instance of the Computer class based on the model defined in Models/Computer.cs
            Computer myComputer = new Computer()
            {
                Motherboard = "Z340",
                CPUCores = 4,
                HasWifi = false,
                HasLTE = true,
                ReleaseDate = DateTime.Now,
                Price = 500.99m,
                VideoCard = "RTX 1060"
            };

            string sql = @"INSERT INTO TutorialAppSchema.Computer(
                Motherboard,
                CPUCores,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price, 
                VideoCard
            )VALUES('" + myComputer.Motherboard
                + "','" + myComputer.CPUCores
                + "','" + myComputer.HasWifi
                + "','" + myComputer.HasLTE
                + "','" + myComputer.ReleaseDate.ToString("yyyy-MM-dd HH:mm:ss")
                + "','" + myComputer.Price
                + "','" + myComputer.VideoCard
            + "')";

            // Console.WriteLine($"SQL Command: {sql}");

            // Dapper call execute method to insert the computer data into the database
            /*
            int result = dbConnection.Execute(sql);
            Console.WriteLine($"Number of rows affected: {result}");
            */

            // 06 Using Dapper with class models
            bool resultStatus = dapper.ExecuteSql(sql);
            // int resultRowCount = dapper.ExecuteSqlWithRowCount(sql); // Alternative

            // 04 Retrieve data from the database using query method with parameters
            // Good practice: <table name>.<column name>
            string sqlSelect = @"
            SELECT
                Computer.Motherboard,
                Computer.CPUCores,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price, 
                Computer.VideoCard
            FROM TutorialAppSchema.Computer";

            // Dapper call query method to retrieve all computers from the database
            /*
            IEnumerable<Computer> computers = dbConnection.Query<Computer>(sqlSelect);
            */

            // 07 Using Dapper with class models
            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

            Console.WriteLine("Computers in the database:");
            
            Console.WriteLine("-------------------1st Method------------------------");
            Console.WriteLine("Motherboard, CPU Cores, Has Wifi, Has LTE, Release Date, Price, Video Card");

            // 1st method to display the retrieved data
            foreach (Computer computer in computers)
            {
                Console.WriteLine("'" + computer.Motherboard
                + "','" + computer.CPUCores
                + "','" + computer.HasWifi
                + "','" + computer.HasLTE
                + "','" + computer.ReleaseDate
                + "','" + computer.Price
                + "','" + computer.VideoCard
                + "'");
            }

            // 2nd method to display the retrieved data
            // Console.WriteLine("-------------------2nd Method------------------------");
            // foreach (Computer computer in computers)
            // {
            //     Console.WriteLine($"Motherboard: {computer.Motherboard}, CPU Cores: {computer.CPUCores}, Has Wifi: {computer.HasWifi}, Has LTE: {computer.HasLTE}, Release Date: {computer.ReleaseDate}, Price: {computer.Price:C}, Video Card: {computer.VideoCard}");
            // }

        }
    }
}