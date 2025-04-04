namespace AstroHunt.API.Models
{
    public class User
    {
        public int Id { get; set; } // Primary Key

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string? Bio {  get; set; }

        public string? profileImageUrl { get; set; }

        public string Role { get; set; } = "User";


        // 🔗 One user ➝ many watchlist items
        public List<WatchlistItem> Watchlist { get; set; } = new();

    }
}
