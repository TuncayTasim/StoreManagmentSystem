
using Microsoft.EntityFrameworkCore;
using StoreManagmentSystem.Data;
using StoreManagmentSystem.Data.Entities;

namespace StoreManagmentSystem.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
