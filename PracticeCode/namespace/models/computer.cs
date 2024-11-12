// Everytime we make cs code, must name the namespace
namespace namespacePractice.models{
    public class Computer{
        public string Motherboard{get; set;} = ""; // Property to create field for class
        public int CPUCores {get; set;} 
        public bool HasWifi {get; set;} 
        public bool HasLTE {get; set;}
        public DateTime ReleaseDate {get; set;} 
        public decimal Price {get; set;} 
        public string VideoCard{get; set;} = ""; // Declare default value for string to avoid null
    }
}