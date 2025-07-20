using System.Data;
using System.Diagnostics.Contracts;
using Dapper;
using FileRWExample.Models; 
using FileRWExample.DatabaseLogic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FileRWExample
{
    // Main Class serves as the entry point for the application
    internal class Program
    {
        static void Main(string[] args)
        {
            // Model data
            Computer myComputer = new Computer()
            {
                ComputerId = 0,
                Motherboard = "Z340",
                CPUCores = 4,
                HasWifi = false,
                HasLTE = true,
                ReleaseDate = DateTime.Now,
                Price = 500.99m,
                VideoCard = "RTX 1060"
            };

            // Data
            string sql = "\n" + @"INSERT INTO TutorialAppSchema.Computer(
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
            + "')\n";

            // Write text to a file
            File.WriteAllText("log.txt", sql); // Overwrites the file with new content

            // Write to a text file using StreamWriter
            // Add new line to the file instead of overwriting
            using StreamWriter openFile = new("log.txt", append: true);
            openFile.WriteLine(sql);

            // Read the file content
            openFile.Close(); // Close the StreamWriter to ensure all data is flushed to the file
            Console.WriteLine("Data written to log.txt.");

            // Read and display the content of the file
            string fileContent = File.ReadAllText("log.txt");
            Console.WriteLine("Reading from log.txt:");
            Console.WriteLine(fileContent);
        }
    }
}