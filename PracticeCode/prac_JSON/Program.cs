using System;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using Dapper;
using prac_JSON.models; //format: main namespace.folder containing separate class
using Microsoft.Data.SqlClient;
using prac_JSON.data;
using System.Runtime.Serialization;
using System.Globalization;
using Microsoft.Extensions.Configuration;


namespace prac_JSON
{   
    internal class Program
    {
        // Main Method
        static void Main(string[] args)
        {
            // Console.WriteLine(rightNow.ToString());

            // Declare model variables
            Computer myComputer = new Computer()
            {
                Motherboard = "Z690",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.87m, //m for decimal
                VideoCard = "RTX 2060"
            };

            // Write this to a text file
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

            // How to write to a file //
            // Write to log file
            // If trigger this command again, it will overwrite the previous log.txt
            File.WriteAllText("log.txt", "\n" + sql + "\n");

            // Write using StreamWriter. Use append argument to ensure it doesn't overwrite
            using StreamWriter openFile = new StreamWriter("log.txt", append: true);

            openFile.WriteLine("\n" + sql);

            openFile.Close(); // Close the file after writing to release the lock to the file

            // How to read from a file //
            Console.WriteLine("Reading from log.txt");
            string fileText = File.ReadAllText("log.txt");
            Console.WriteLine(fileText);

        }
    }
}
