using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AstroHunt.API.Models;
using AstroHunt.API.Services;

namespace AstroHunt.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WatchlistController : ControllerBase
    {
        private readonly IAuthService _authService;

        public WatchlistController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWatchlist()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var items = await _authService.GetWatchlistAsync(userId);
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWatchlist(AddWatchlistItemDto dto)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _authService.AddWatchlistItemAsync(userId, dto);
            return Ok(new { message = "Item added to watchlist" });
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteFromWatchlist(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var deleted = await _authService.DeleteWatchlistItemAsync(userId, id);

            if (!deleted) return NotFound(new { message = "Item not found or not yours to delete" });

            return Ok(new { message = "Item deleted successfully" });
        }
    }
}
