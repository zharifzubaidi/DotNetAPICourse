namespace database.models
{
    // Model
    public class Computer
    {
        public string Motherboard { get; set; } = ""; // To avoid null
        public int CPUCores { get; set; }
        public bool HasWifi { get; set; }
        public bool HasLTE { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string VideoCard { get; set; } = "";  // To avoid null
    }
}