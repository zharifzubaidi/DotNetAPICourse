using System.Data;
using System.Diagnostics.Contracts;
using Dapper;
using EFramework.Models; // namespace <project name>.<folder name for the model or class name>
using EFramework.DatabaseLogic;
using Microsoft.Data.SqlClient;

namespace DapperExample
{
    // Main Class serves as the entry point for the application
    internal class Program
    {
        static void Main(string[] args)
        {
            DataContextDapper dapper = new DataContextDapper();
            DateTime rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
            Console.WriteLine($"Current Date and Time: {rightNow}");

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

            bool resultStatus = dapper.ExecuteSql(sql);

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

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

            Console.WriteLine("Computers in the database:");
            
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("'Motherboard', 'CPU Cores', 'Has Wifi', 'Has LTE', 'Release Date', 'Price', 'Video Card'");
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
        }
    }
}