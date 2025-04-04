namespace AstroHunt.API.Models
{
    public class UserProfileDto
    {
        public string Username { get; set; } = string.Empty;
        public string? Bio {  get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
