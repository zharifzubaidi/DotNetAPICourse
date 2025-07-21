

using System.Text.Json.Serialization;

namespace ModelMap.Models // namespace <project name>.<folder name for the model or class name>
{
    // Creating computer class as a model representing a computer's specifications
    // This class encapsulates the properties of a computer, such as motherboard, CPU cores, and more
    public class Computer
    {
        // To use JSON attributes for mapping. Apply to row below jsonproperty from System.Text.Json //
        [JsonPropertyName("computer_id")]
        public int ComputerId { get; set; } // Primary key for the Computer model, auto-incremented by EF Core

        [JsonPropertyName("motherboard")]
        public string Motherboard { get; set; } = string.Empty; // Use an empty string as default value to handle nullability

        [JsonPropertyName("cpu_cores")]
        public int? CPUCores { get; set; } = 0; // Nullable int for CPU cores, default is 0

        [JsonPropertyName("has_wifi")]
        public bool HasWifi { get; set; }

        [JsonPropertyName("has_lte")]
        public bool HasLTE { get; set; }

        [JsonPropertyName("release_date")]
        public DateTime? ReleaseDate { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("video_card")]
        public string VideoCard { get; set; } = string.Empty;   // Use an empty string as default value to handle nullability
    }
}