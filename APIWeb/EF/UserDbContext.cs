using APIWeb.Entities;
using Microsoft.EntityFrameworkCore;
namespace APIWeb.EF
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) 
        {
        
        }
        public DbSet<User> Users { get; set; }
    }
}
