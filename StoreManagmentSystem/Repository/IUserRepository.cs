using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models;

namespace StoreManagmentSystem.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> AddUser(UserModelWithPass user);
        Task DeleteUser(User user);
        Task UpdateUser(User user);

        Task<User> GetUserById(Guid Id);
        Task<User> GetUserByEmail(string Email);
        Task<User> GetUserByToken(string Token);
        
        Task<UserModelWithRole> GetModelById(Guid Id);
        
    }
}
