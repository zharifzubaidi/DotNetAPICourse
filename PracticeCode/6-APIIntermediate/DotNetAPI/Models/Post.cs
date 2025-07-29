namespace DotNetAPI.Models
{
    public partial class Post
    {
        // Model fields for the Posts table
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string PostTitle { get; set; } = null!;
        public string PostContent { get; set; } = null!;
        public DateTime? PostCreated { get; set; }
        public DateTime? PostUpdated { get; set; }
    }
}