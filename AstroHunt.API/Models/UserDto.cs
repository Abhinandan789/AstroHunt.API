namespace AstroHunt.API.Models
{
    public class UserDto
    {
            public int Id { get; set; }
            public string Username { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Role { get; set; } = "User";
            public string Bio { get; set; } = string.Empty;
            public string ProfileImageUrl { get; set; } = string.Empty;
        }
}
