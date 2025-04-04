using AstroHunt.API.Models;
using AstroHunt.API.Data;
using Microsoft.EntityFrameworkCore;

namespace AstroHunt.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AstroHuntDbContext _context;

        public UserRepository(AstroHuntDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
