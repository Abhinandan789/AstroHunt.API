using AstroHunt.API.Models;

namespace AstroHunt.API.Services
{
    public interface IAuthService
    {
        Task<string> RegisterUserAsync(RegisterDto request);
        Task<string> LoginUserAsync(LoginDto request);

        Task<UserProfileDto?> GetUserProfileAsync(int userId);
        Task<bool> UpdateUserProfileAsync(int userId, UserProfileDto profileDto);


        Task<List<WatchlistItemDto>> GetWatchlistAsync(int userId);
        Task AddWatchlistItemAsync(int userId, AddWatchlistItemDto dto);


        Task<bool> DeleteWatchlistItemAsync(int userId, int itemId);


        //for ADMIN
        Task<List<UserDto>> GetAllUsersAsync();

        Task<List<WatchlistSummaryDto>> GetWatchlistSummaryAsync();



    }
}
