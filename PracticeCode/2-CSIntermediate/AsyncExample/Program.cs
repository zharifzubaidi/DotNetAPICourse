using System.Data;
using System.Diagnostics.Contracts;
using Dapper;
using AsyncExample.Models; 
using AsyncExample.DatabaseLogic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AsyncExample
{
    // Model map method
    // Automapper
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
            string computerJson = File.ReadAllText("ComputersSnake.json"); // Data is in snake_case format

            // -----------------------------Method 1: Automapper-----------------------------
            // Map destination based mode to the source based model
            // ComputerSnake is the source model with snake_case properties
            // Computer is the destination model with PascalCase properties
            // Add LoggerFactory to the MapperConfiguration

            // -----------------------------Old way of mapping using Automapper-----------------------------//
            /*
            var loggerFactory = LoggerFactory.Create(static builder =>
            {
                builder
                    .AddConsole()
                    .SetMinimumLevel(LogLevel.Debug);
            });

            Mapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ComputerSnake, Computer>()
                    .ForMember(destination => destination.ComputerId, options =>
                        options.MapFrom(source => source.computer_id))
                    .ForMember(destination => destination.Motherboard, options =>
                        options.MapFrom(source => source.motherboard))
                    .ForMember(destination => destination.CPUCores, options =>
                        options.MapFrom(source => source.cpu_cores))
                    .ForMember(destination => destination.HasWifi, options =>
                        options.MapFrom(source => source.has_wifi))
                    .ForMember(destination => destination.HasLTE, options =>
                        options.MapFrom(source => source.has_lte))
                    .ForMember(destination => destination.ReleaseDate, options =>
                        options.MapFrom(source => source.release_date))
                    .ForMember(destination => destination.Price, options =>
                        options.MapFrom(source => source.price))
                    .ForMember(destination => destination.VideoCard, options =>
                        options.MapFrom(source => source.video_card));
            }, loggerFactory));

            IEnumerable<ComputerSnake>? computersSnakeSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computerJson);

            if (computersSnakeSystem != null)
            {
                IEnumerable<ComputerSnake> computerResult = mapper.Map<IEnumerable<ComputerSnake>>(computersSnakeSystem);
                Console.WriteLine("---------Old AutoMapper mapping---------");
                foreach (ComputerSnake singleComputer in computerResult)
                {
                    Console.WriteLine($"Found computer using AutoMapper: {singleComputer.motherboard}");
                }
            }
            else
            {
                Console.WriteLine("No computers found in the JSON file.");
                return;
            }
            */
            // -----------------------------Old way of mapping using Automapper-----------------------------//

            // -----------------------------New way of mapping using Automapper-----------------------------//
            
            var loggerNewFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddConsole()
                    .SetMinimumLevel(LogLevel.Debug);
            });

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ComputerSnake, Computer>()
                    .ForMember(dest => dest.ComputerId, opt => opt.MapFrom(src => src.computer_id))
                    .ForMember(dest => dest.Motherboard, opt => opt.MapFrom(src => src.motherboard))
                    .ForMember(dest => dest.CPUCores, opt => opt.MapFrom(src => src.cpu_cores))
                    .ForMember(dest => dest.HasWifi, opt => opt.MapFrom(src => src.has_wifi))
                    .ForMember(dest => dest.HasLTE, opt => opt.MapFrom(src => src.has_lte))
                    .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.release_date))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.price))
                    .ForMember(dest => dest.VideoCard, opt => opt.MapFrom(src => src.video_card));
            }, loggerNewFactory);

            IMapper mapperNew = mapperConfig.CreateMapper();

            IEnumerable<ComputerSnake>? computersNewSnakeSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computerJson);

            if (computersNewSnakeSystem != null)
            {
                IEnumerable<ComputerSnake> computerNewResult = mapperNew.Map<IEnumerable<ComputerSnake>>(computersNewSnakeSystem);
                Console.WriteLine("------------------New AutoMapper mapping------------------");
                Console.WriteLine("Automapper Property Count: " + computerNewResult.Count());
                // foreach (ComputerSnake singleComputer in computerNewResult)
                // {
                //     Console.WriteLine($"Found computer using AutoMapper: {singleComputer.motherboard}");
                // }
            }
            else
            {
                Console.WriteLine("No computers found in the JSON file.");
                return;
            }

            // -----------------------------New way of mapping using Automapper-----------------------------//



            // -----------------------------Method 2: JSON Attribute Mapping-----------------------------
            // Need to be setup in our Computer and ComputerSnake models
            IEnumerable<Computer>? computersJsonSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computerJson);
            if (computersJsonSystem != null)
            {
                
                Console.WriteLine("------------------Json attribute mapping------------------");
                Console.WriteLine("JSON Property Count: " + computersJsonSystem.Count());
                // foreach (Computer singleComputer in computersJsonSystem)
                // {
                //     Console.WriteLine($"Found computer using Json attributes: {singleComputer.Motherboard}");
                // }
            }
            else
            {
                Console.WriteLine("No computers found in the JSON file.");
                return;
            }
        }
    }
}