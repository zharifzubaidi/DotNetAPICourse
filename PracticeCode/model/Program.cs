using System;
using System.Net.NetworkInformation;


namespace model{
    // Model practice code
    // Create new model class to hold data
    public class Computer{
        public string Motherboard{get; set;} = ""; // Property to create field for class
        public int CPUCores {get; set;} 
        public bool HasWifi {get; set;} 
        public bool HasLTE {get; set;}
        public DateTime ReleaseDate {get; set;} 
        public decimal Price {get; set;} 
        public string VideoCard{get; set;} = ""; // Declare default value for string to avoid null

        // public Computer(){
        //     // Create constructable to ensure string variable to not equal to null
        //     if(VideoCard == null){
        //         VideoCard = "";
        //     }
        //     if(Motherboard == null){
        //         Motherboard = "";
        //     }
        // }
    }

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