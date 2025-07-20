using System.Data;
using System.Diagnostics.Contracts;
using Dapper;
using ConfigExample.Models; // namespace <project name>.<folder name for the model or class name>
using ConfigExample.DatabaseLogic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ConfigExample
{
    // Main Class serves as the entry point for the application
    internal class Program
    {
        static void Main(string[] args)
        {
            // Extracting data from the appsettings.json file
            // IConfiguration will search for ConnectionStrings in the appsettings.json file
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Set the base path to the current directory
                .AddJsonFile("appsettings.json")
                .Build();

            // Dapper Example - has more control over SQL queries and is generally faster for simple operations
            // This example demonstrates how to use Dapper to interact with a SQL Server database.
            DataContextDapper dapper = new DataContextDapper(config);
            DateTime rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
            Console.WriteLine($"Current Date and Time: {rightNow}");

            // Entity Framework Example
            // This example demonstrates how to use Entity Framework to interact with a SQL Server database.
            DataContextEFramework entityFramework = new DataContextEFramework(config);

            // Model data
            Computer myComputer = new Computer()
            {
                ComputerId = 0, // EF Core will auto-generate this ID
                Motherboard = "Z340",
                CPUCores = 4,
                HasWifi = false,
                HasLTE = true,
                ReleaseDate = DateTime.Now,
                Price = 500.99m,
                VideoCard = "RTX 1060"
            };

            // Add the computer to the database using Entity Framework
            entityFramework.Add(myComputer);
            entityFramework.SaveChanges();

            // Data to insert into the database (Insert)
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

            // Dapper example to execute the SQL command
            bool resultStatus = dapper.ExecuteSql(sql);

            // Entity Framework example to execute the SQL command

            // Dapper example
            /*
            // Data to select from the database (Query) 
            string sqlSelect = @"
            SELECT
                Computer.ComputerId,
                Computer.Motherboard,
                Computer.CPUCores,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price, 
                Computer.VideoCard
            FROM TutorialAppSchema.Computer";
            
            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

            Console.WriteLine("Dapper example:");

            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("'ComputerId','Motherboard', 'CPU Cores', 'Has Wifi', 'Has LTE', 'Release Date', 'Price', 'Video Card'");
            foreach (Computer computer in computers)
            {
                Console.WriteLine("'" + computer.ComputerId
                + "','" + computer.Motherboard
                + "','" + computer.CPUCores
                + "','" + computer.HasWifi
                + "','" + computer.HasLTE
                + "','" + computer.ReleaseDate
                + "','" + computer.Price
                + "','" + computer.VideoCard
                + "'");
            }
            */
            
            // Entity Framework example
            IEnumerable<Computer>? computersEF = entityFramework.Computer?.ToList<Computer>();
            if (computersEF != null)
            {
                Console.WriteLine("Entity framework example:");
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("'ComputerId','Motherboard', 'CPU Cores', 'Has Wifi', 'Has LTE', 'Release Date', 'Price', 'Video Card'");
                foreach (Computer singleComputer in computersEF)
                {
                    Console.WriteLine("'" + singleComputer.ComputerId
                    + "','" + singleComputer.Motherboard
                    + "','" + singleComputer.CPUCores
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
}