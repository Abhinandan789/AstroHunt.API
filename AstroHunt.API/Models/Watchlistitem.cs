namespace AstroHunt.API.Models
{
    public class WatchlistItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Type { get; set; } // e.g. Planet, Asteroid

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        // 🔗 Relationship to the User
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
