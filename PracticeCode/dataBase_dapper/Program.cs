﻿using System;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using Dapper;
using database.models; //format: main namespace.folder containing separate class
using Microsoft.Data.SqlClient;
using database.data;
using System.Runtime.Serialization;
using System.Globalization;


namespace dataBase
{   
    internal class Program
    {
        // Main Method
        static void Main(string[] args)
        {
            // Create instance of datacontextdapper class
            DataContextDapper dapper = new DataContextDapper();

            // Entity Framework 
            DataContextEF entityFramework = new DataContextEF();

            // Dapper sql call: query. Query statement.
            string sqlCommand = "SELECT GETDATE()";
            DateTime rightNow = dapper.LoadDataSingle<DateTime>(sqlCommand); // will return a single row of the query

            //Console.WriteLine(rightNow.ToString());

            // Define model variables
            Computer myComputer = new Computer()
            {
                Motherboard = "Z690",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.87m, //m for decimal
                VideoCard = "RTX 2060"
            };

            // Entity framework Add a new row to the table based on top model
            entityFramework.Add(myComputer);
            entityFramework.SaveChanges();

            // Writing to database
            // Writing sql INSERT statement to inserting data into SQL table. This command is to enter the header for each column in a sql table
            string sql = @"INSERT INTO TutorialAppSchema.Computer(
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
                    + "','" + myComputer.Price.ToString("0.00", CultureInfo.InvariantCulture)
                    + "','" + myComputer.VideoCard
                    + "')";
            //Console.WriteLine(sql);

            // Dapper call: Execute. Insert statement. This function will return how many rows affected
            bool result = dapper.ExecuteSql(sql);
            //Console.WriteLine(result);

            // Reading from database //
            // Dapper call: Query. Select statement. This function is to get a row from the table.
            string sqlSelect = @"
            SELECT
                Computer.ComputerId,
                Computer.Motherboard,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard
            FROM TutorialAppSchema.Computer";

            // Dapper application. Will receive IEnumerable from query command. Alternatively, we can cast the return value to list. dbConnection.Query<Computer>(sqlSelect).ToList();
            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

            Console.WriteLine("'ComputerId','Motherboard','HasWifi','HasLTE','ReleaseDate','Price','VideoCard'");

            foreach (Computer singleComputer in computers)
            {
                Console.WriteLine("'" + singleComputer.ComputerId
                    + "','" + singleComputer.Motherboard
                    + "','" + singleComputer.HasWifi
                    + "','" + singleComputer.HasLTE
                    + "','" + singleComputer.ReleaseDate.ToString("yyyy-MM-dd")
                    + "','" + singleComputer.Price.ToString("0.00", CultureInfo.InvariantCulture)
                    + "','" + singleComputer.VideoCard
                    + "'");
            }

            // Entity framework application
            IEnumerable<Computer>? computersEF = entityFramework.Computer?.ToList<Computer>();

            if (computersEF != null)
            {
                Console.WriteLine("'ComputerId','Motherboard','HasWifi','HasLTE','ReleaseDate','Price','VideoCard'");

                foreach (Computer singleComputer in computersEF)
                {
                    Console.WriteLine("'" + singleComputer.ComputerId
                        + "','" + singleComputer.Motherboard
                        + "','" + singleComputer.HasWifi
                        + "','" + singleComputer.HasLTE
                        + "','" + singleComputer.ReleaseDate.ToString("yyyy-MM-dd")
                        + "','" + singleComputer.Price.ToString("0.00", CultureInfo.InvariantCulture)
                        + "','" + singleComputer.VideoCard
                        + "'");
                }
            }

            //Console.WriteLine(myComputer.Motherboard);
            //Console.WriteLine(myComputer.HasWifi);
            //Console.WriteLine(myComputer.HasWifi);
            //Console.WriteLine(myComputer.ReleaseDate);
            //Console.WriteLine(myComputer.Price);
            //Console.WriteLine(myComputer.VideoCard);
        }
    }
}
