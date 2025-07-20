using System.Diagnostics.Contracts;
using ModelsExample.Models; // namespace <project name>.<folder name for the model or class name>

namespace ModelsExample
{

    // Creating computer class as a model representing a computer's specifications
    // This class encapsulates the properties of a computer, such as motherboard, CPU cores, and more
    // The class move to a model folder
    // public class Computer
    // {
    //     // Need to use public attributes to allow access from other classes
    //     // Fields should be private to encapsulate data _motherboard
    //     // Properties should be used for public access Motherboard{get{return _motherboard}; set{_motherboard = value;}}
    //     // When we try to use field, we should access it through the property
    //     // But C# has evolved to take care in the background. We can now just use private string Motherboard{get; set;};

    //     // private string? Motherboard { get; set; }            // 1st option: For string type, need a nullable type by adding '?'
    //     public string Motherboard { get; set; } = string.Empty; // 2nd option: Use an empty string as default value to handle nullability
    //     public int CPUCores { get; set; }
    //     public bool HasWifi { get; set; }
    //     public bool HasLTE { get; set; }
    //     public DateTime ReleaseDate { get; set; }
    //     public decimal Price { get; set; }
    //     public string VideoCard { get; set; } = string.Empty;   // 2nd option: Use an empty string as default value to handle nullability

    // }

    // Main Class serves as the entry point for the application
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the Computer class
            Computer myComputer = new Computer()
            {
                Motherboard = "Z690",
                CPUCores = 8,
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 1499.99m,
                VideoCard = "RTX 2060"
            };

            // Display the computer's properties
            Console.WriteLine("Computer Specifications:");
            Console.WriteLine($"Motherboard: {myComputer.Motherboard}");
            Console.WriteLine($"CPU Cores: {myComputer.CPUCores}");
            Console.WriteLine($"Has WiFi: {myComputer.HasWifi}");
            Console.WriteLine($"Has LTE: {myComputer.HasLTE}");
            Console.WriteLine($"Release Date: {myComputer.ReleaseDate.ToShortDateString()}");   // Displaying only the date part
            Console.WriteLine($"Price: {myComputer.Price:C}");                                  // Displaying price in local currency format
            Console.WriteLine($"Video Card: {myComputer.VideoCard}");
        }
    }
}