namespace AstroHunt.API.Models
{
    public class WatchlistItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Type { get; set; }
        public string? Description { get; set; }   
        public string? ImageUrl { get; set; }
    }
}
