
using Microsoft.EntityFrameworkCore;
using StoreManagmentSystem.Data;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models;

namespace StoreManagmentSystem.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddUser(UserModelWithPass user)
        {
            var NewUser = new User
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RoleId = user.RoleId,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Note = user.Note,
                Password = user.Password,
                ActionToken = "",
                StatusId = 1
            };


            await _context.Users.AddAsync(NewUser);
            await _context.SaveChangesAsync();
            return NewUser;
        }

        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users
        .AsNoTracking()
        .Select(u => new User
        {
            UserId = u.UserId,
            UserName = u.UserName,
            FirstName = u.FirstName,
            LastName = u.LastName,
            RoleId = u.RoleId,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber,
            Note = u.Note,
            StatusId = u.StatusId

        })
        .ToListAsync();
        }

        public async Task<User> GetUserById(Guid Id)
        {
            return await _context.Users.FindAsync(Id);
        }
        public async Task<User> GetUserByEmail(string Email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == Email);
        }
        public async Task<User> GetUserByToken(string Token)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.ActionToken == Token);
        }

        public async Task<UserModelWithRole> GetModelById(Guid Id)
        {
            var user = await _context.Users.FindAsync(Id);
            return MapToModel(user);
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

         public UserModelWithRole MapToModel(User entity)
        {
            return new UserModelWithRole
            {
                UserName = entity.UserName,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                RoleId = entity.RoleId,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                Note = entity.Note
            };
        }
    }
}
