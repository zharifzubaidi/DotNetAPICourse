namespace DotNetAPI.Dtos
{
    public partial class UserForRegistrationDto
    {
        // Getter and setter to make a property accessible
        public string Email { get; set; } = "";  
        public string? FirstName { get; set; } = "";
        public string? LastName { get; set; } = "";
        public string? Gender { get; set; } = "";  
        public string Password { get; set; } = "";
        public string PasswordConfirm { get; set; } = "";
    }
}