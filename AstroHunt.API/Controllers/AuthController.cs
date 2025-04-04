using Microsoft.AspNetCore.Mvc;
using AstroHunt.API.Models;
using AstroHunt.API.Services;
using Microsoft.AspNetCore.Authorization;

namespace AstroHunt.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto request)
        {
            var result = await _authService.RegisterUserAsync(request);

            if (result == "User registered successfully.")
                return Ok(new { message = result });

            return BadRequest(new { error = result });
        }
    


    [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            var result = await _authService.LoginUserAsync(request);

            if (result == "Invalid email or password.")
                return Unauthorized(new { error = result });

            return Ok(new { token = result });
        }


        [HttpGet("me")]
        [Authorize]
        public IActionResult GetMe()
        {
            var username = User.Identity?.Name;
            return Ok(new { message = $"Welcome back, {username}!" });
        }


    }
}
