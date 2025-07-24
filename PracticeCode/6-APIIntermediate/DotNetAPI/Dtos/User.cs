namespace DotNetAPI.UserDtos
{
    public partial class UserToAddDto
    {
        //public int UserId { get; set; } Remove primary key from DTO
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Gender { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}
