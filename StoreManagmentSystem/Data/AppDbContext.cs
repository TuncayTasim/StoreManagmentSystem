using Microsoft.EntityFrameworkCore;
using StoreManagmentSystem.Data.Entities;

namespace StoreManagmentSystem.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
    }
}
