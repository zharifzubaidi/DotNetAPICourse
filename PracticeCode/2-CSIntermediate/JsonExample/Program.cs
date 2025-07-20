using System.Data;
using System.Diagnostics.Contracts;
using Dapper;
using JsonExample.Models; 
using JsonExample.DatabaseLogic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JsonExample
{
    // Main Class serves as the entry point for the application
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a configuration object to read the appsettings.json file
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Create an instance of DataContextDapper using the configuration
            DataContextDapper dapper = new DataContextDapper(config);

            // Load data from Json file
            string computerJson = File.ReadAllText("Computers.json");
            // Console.WriteLine(computerJson);

            // -----------------------Deserialize the JSON data into a list of Computer objects topic --------------------
            // 1st JSON deserialization method using System.Text.Json
            // Need to access at key-value pairs and map to the Computer model
            // All json file has camelCase properties. We need to convert them to PascalCase to match the Computer model
            // For System.Text.Json, we can use JsonSerializerOptions to set the property naming policy
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Convert camelCase (Json) to PascalCase (C#)
            };

            IEnumerable<Computer>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computerJson, options);
            if (computersSystem != null)
            {
                foreach (Computer singleComputer in computersSystem)
                {
                    //Console.WriteLine($"Found computer using System.Text.Json: {singleComputer.Motherboard}");
                    // Load data into a SQL command to insert into the database
                    string sql = @"INSERT INTO TutorialAppSchema.Computer(
                        Motherboard,
                        CPUCores,
                        HasWifi,
                        HasLTE,
                        ReleaseDate,
                        Price, 
                        VideoCard
                    )VALUES('" + EscapeSingleQuotes(singleComputer.Motherboard)
                        + "','" + singleComputer.CPUCores
                        + "','" + singleComputer.HasWifi
                        + "','" + singleComputer.HasLTE
                        + "','" + singleComputer.ReleaseDate?.ToString("yyyy-MM-dd")
                        + "','" + singleComputer.Price
                        + "','" + EscapeSingleQuotes(singleComputer.VideoCard)
                    + "')";

                    // Execute the SQL command to insert the computer data into the database
                    dapper.ExecuteSql(sql);

                }
            }
            else
            {
                Console.WriteLine("No computers found in the JSON file.");
                return;
            }

            // 2nd JSON deserialization method using Newtonsoft.Json
            // Newtonsoft.Json is another popular library for JSON handling in C#
            IEnumerable<Computer>? computersNewton = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computerJson);

            if (computersNewton != null)
            {
                foreach (Computer singleComputerNewton in computersNewton)
                {
                    // Console.WriteLine($"Found computer using Newtonsoft.Json: {singleComputerNewton.Motherboard}");

                }
            }
            else
            {
                Console.WriteLine("No computers found in the JSON file.");
                return;
            }

            // -------------------------------Serialize the data back to JSON ----------------------------
            // Need to ensure in camelCase format for JSON output
            // System.Text.Json
            string computersCopySystemText = System.Text.Json.JsonSerializer.Serialize(computersSystem, options);
            File.WriteAllText("ComputersCopySystemText.txt", computersCopySystemText);

            // Newtonsoft.Json
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            string computersCopyNewtonSoft = JsonConvert.SerializeObject(computersNewton, settings);
            File.WriteAllText("ComputersCopyNewtonSoft.txt", computersCopyNewtonSoft);

        }

        static string EscapeSingleQuotes(string input)
        {
            // Escape single quotes by replacing them with two single quotes
            string output = input.Replace("'", "''"); // Replace single quote with two single quotes
            // This is necessary for SQL queries to avoid syntax errors
            // Example: O'Reilly becomes O''Reilly
            return output;
        }
    }
}