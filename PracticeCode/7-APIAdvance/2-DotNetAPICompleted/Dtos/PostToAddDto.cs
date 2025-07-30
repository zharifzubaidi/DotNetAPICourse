namespace DotNetAPI.Models
{
    public partial class PostToAddDto
    {
        // Model fields for the Posts table
        public string PostTitle { get; set; } = null!;
        public string PostContent { get; set; } = null!;
    }
}