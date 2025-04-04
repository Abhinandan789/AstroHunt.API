using AstroHunt.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AstroHunt.API.Data
{
    public class AstroHuntDbContext  : DbContext
    {
        public AstroHuntDbContext(DbContextOptions<AstroHuntDbContext> options) : base(options) 
        {
            
        } 

        public DbSet<User> Users { get; set; }
    }
}
