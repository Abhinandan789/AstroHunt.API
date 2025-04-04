using AstroHunt.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAuthService _authService;

    public AdminController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet("test")]
    [Authorize(Roles = "Admin")]
    public IActionResult AdminTest()
    {
        return Ok(new { message = "You are an admin and authorized!" });
    }


    [HttpGet("users")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _authService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("watchlist/summary")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetWatchlistSummary()
    {
        var summary = await _authService.GetWatchlistSummaryAsync();
        return Ok(summary);
    }


}
