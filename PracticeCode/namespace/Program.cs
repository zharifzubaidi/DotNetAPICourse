using System;
using System.Net.NetworkInformation;
// Importing other cs file using namespace
using namespacePractice.models;

namespace namespacePractice{
    // Namespace practice code
    internal class Program{
        static void Main(string[] args) { 
            // Declaring instance for model class
            Computer myComputer = new Computer(){
                Motherboard = "Z690",
                HasWifi = true,
                HasLTE = true,
                ReleaseDate = DateTime.Now,
                Price = 943.87m,
                VideoCard = "RTX 2060"
            };
            // Alter value after declaring model
            myComputer.HasWifi = false;
            // Output attributes of model class
            Console.WriteLine(myComputer.Motherboard);
            Console.WriteLine(myComputer.HasWifi);
            Console.WriteLine(myComputer.HasLTE);
            Console.WriteLine(myComputer.ReleaseDate);
            Console.WriteLine(myComputer.Price);
            Console.WriteLine(myComputer.VideoCard);

        }
    }
}