using AstroHunt.API.Models;

namespace AstroHunt.API.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserExistsByEmailAsync(string email);

        Task CreateUserAsync(User user);

        Task<User?> GetUserByEmailAsync(string email);

        Task<User?> GetUserByIdAsync(int userId);
        Task<bool> SaveChangesAsync();


        Task<List<WatchlistItem>> GetWatchlistAsync(int userId);
        Task AddWatchlistItemAsync(WatchlistItem item);


        Task<WatchlistItem?> GetWatchlistItemByIdAsync(int itemId);
        Task<bool> DeleteWatchlistItemAsync(WatchlistItem item);


    }
}
