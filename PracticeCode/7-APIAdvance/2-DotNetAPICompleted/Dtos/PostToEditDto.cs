namespace DotNetAPI.Models
{
    public partial class PostToEditDto
    {
        // Model fields for the Posts table
        public int PostId { get; set; }
        public string PostTitle { get; set; } = null!;
        public string PostContent { get; set; } = null!;
    }
}