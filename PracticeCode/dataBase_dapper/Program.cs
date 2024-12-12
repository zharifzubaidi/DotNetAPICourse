using System;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using Dapper;
using database.models; //format: main namespace.folder containing separate class
using Microsoft.Data.SqlClient;


namespace dataBase
{   
    internal class Program
    {
        // Main Method
        static void Main(string[] args)
        {
            // Connecting to SQL server
            string connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";
            IDbConnection dbConnection = new SqlConnection(connectionString);
            
            // Dapper sql query
            string sqlCommand = "SELECT GETDATE()";
            DateTime rightNow = dbConnection.QuerySingle<DateTime>(sqlCommand); // will return a single row of the query

            Console.WriteLine(rightNow);


            //Computer myComputer = new Computer()
            //{
            //    Motherboard = "Z690",
            //    HasWifi = true,
            //    HasLTE = false,
            //    ReleaseDate = DateTime.Now,
            //    Price = 943.87m, //m for decimal
            //    VideoCard = "RTX 2060"
            //};
            //Console.WriteLine(myComputer.Motherboard);
            //Console.WriteLine(myComputer.HasWifi);
            //Console.WriteLine(myComputer.HasWifi);
            //Console.WriteLine(myComputer.ReleaseDate);
            //Console.WriteLine(myComputer.Price);
            //Console.WriteLine(myComputer.VideoCard);
        }
    }
}
