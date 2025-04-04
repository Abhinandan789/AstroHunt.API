using AstroHunt.API.Models;
using AstroHunt.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace AstroHunt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UserController(IAuthService authService) {
            _authService = authService;
        }

        // Get /api/user/me
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            //this will get the userId from JWT token claim
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            //calling the service to get the user's profile info
            var profile = await _authService.GetUserProfileAsync(userId);

            if(profile == null) return NotFound(new {message = "User not found."});

            return Ok(profile); //Return the profile(username, bio, image URL)

        }

        //Post /api/user/me

        [HttpPost("me")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UserProfileDto profileDto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var updated = await _authService.UpdateUserProfileAsync(userId, profileDto);

            if (!updated) return BadRequest(new { message = "Failed to update the user profile!" });

            return Ok(new { message = "Profile updated successfully!" });
        }
    }
}
