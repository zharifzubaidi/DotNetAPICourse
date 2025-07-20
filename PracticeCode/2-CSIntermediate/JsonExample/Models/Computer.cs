namespace JsonExample.Models // namespace <project name>.<folder name for the model or class name>
{
    // Creating computer class as a model representing a computer's specifications
    // This class encapsulates the properties of a computer, such as motherboard, CPU cores, and more
    public class Computer
    {
        // Need to use public attributes to allow access from other classes
        // Fields should be private to encapsulate data _motherboard
        // Properties should be used for public access Motherboard{get{return _motherboard}; set{_motherboard = value;}}
        // When we try to use field, we should access it through the property
        // But C# has evolved to take care in the background. We can now just use private string Motherboard{get; set;};
        public int ComputerId { get; set; } // Primary key for the Computer model, auto-incremented by EF Core
        public string Motherboard { get; set; } = string.Empty; // Use an empty string as default value to handle nullability
        public int? CPUCores { get; set; } = 0; // Nullable int for CPU cores, default is 0
        public bool HasWifi { get; set; }
        public bool HasLTE { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string VideoCard { get; set; } = string.Empty;   // Use an empty string as default value to handle nullability
    }
}