using Microsoft.EntityFrameworkCore;
using StoreManagmentSystem.Data.Entities;

namespace StoreManagmentSystem.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Entities.Type> Types { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
    }
}
