using System.Security.Cryptography;
using System.Text;
using AstroHunt.API.Data;
using AstroHunt.API.Models;
using AstroHunt.API.Repositories;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;


namespace AstroHunt.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> RegisterUserAsync(RegisterDto request)
        {
            // Check if user exists
            if (await _userRepository.UserExistsByEmailAsync(request.Email))
            {
                return "Email already registered.";
            }

            // Hash the password
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            // Create User object
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            // Save user to DB
            await _userRepository.CreateUserAsync(user);

            return "User registered successfully.";
        }

        // Password hashing using HMACSHA512
        private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }


        public async Task<string> LoginUserAsync(LoginDto request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            if (user == null)
            {
                return "Invalid email or password.";
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return "Invalid email or password.";
            }

            // Generate JWT Token
            var token = CreateJwtToken(user);
            return token;
        }



        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(storedHash);
        }


        private string CreateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),

            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this_is_a_super_secure_jwt_key_for_astrohunt_application_1234567890"));

            // replace with real secret in prod

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        //User details 
        public async Task<UserProfileDto?> GetUserProfileAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return null;

            return new UserProfileDto
            {
                Username = user.Username,
                Bio = user.Bio,
                ProfileImageUrl = user.profileImageUrl
            };
        }

        public async Task<bool> UpdateUserProfileAsync(int userId, UserProfileDto profileDto)
        {
            var user =await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            user.Username = profileDto.Username;
            user.Bio = profileDto.Bio;
            user.profileImageUrl = profileDto.ProfileImageUrl;

            return await _userRepository.SaveChangesAsync();
        }



        //watchlist 
        public async Task<List<WatchlistItemDto>> GetWatchlistAsync(int userId)
        {
            var items = await _userRepository.GetWatchlistAsync(userId);

            return items.Select(item => new WatchlistItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Type = item.Type,
                Description = item.Description,
                ImageUrl = item.ImageUrl
            }).ToList();
        }

        public async Task AddWatchlistItemAsync(int userId, AddWatchlistItemDto dto)
        {
            var item = new WatchlistItem
            {
                Title = dto.Title,
                Type = dto.Type,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                UserId = userId
            };

            await _userRepository.AddWatchlistItemAsync(item);
        }

        public async Task<bool> DeleteWatchlistItemAsync(int userId, int itemId)
        {
            var item = await _userRepository.GetWatchlistItemByIdAsync(itemId);

            if (item == null || item.UserId != userId)
                return false; // Either doesn't exist or doesn't belong to the user

            return await _userRepository.DeleteWatchlistItemAsync(item);
        }



        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();

            return users.Select(user => new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                Bio = user.Bio,
                ProfileImageUrl = user.profileImageUrl
            }).ToList();
        }

        public async Task<List<WatchlistSummaryDto>> GetWatchlistSummaryAsync()
        {
            return await _userRepository.GetWatchlistSummaryAsync();
        }


    }
}
