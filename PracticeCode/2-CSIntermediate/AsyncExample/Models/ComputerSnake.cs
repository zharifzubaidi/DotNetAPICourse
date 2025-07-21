namespace AsyncExample.Models // namespace <project name>.<folder name for the model or class name>
{
    // Creating computer class as a model representing a computer's specifications
    // This class encapsulates the properties of a computer, such as motherboard, CPU cores, and more
    public class ComputerSnake
    {
        // To use JSON attributes for mapping //


        // To use JSON attributes for mapping //
        public int computer_id { get; set; } // Primary key for the Computer model, auto-incremented by EF Core
        public string motherboard { get; set; } = string.Empty; // Use an empty string as default value to handle nullability
        public int? cpu_cores { get; set; } = 0; // Nullable int for CPU cores, default is 0
        public bool has_wifi { get; set; }
        public bool has_lte { get; set; }
        public DateTime? release_date { get; set; }
        public decimal price { get; set; }
        public string video_card { get; set; } = string.Empty;   // Use an empty string as default value to handle nullability
    }
}