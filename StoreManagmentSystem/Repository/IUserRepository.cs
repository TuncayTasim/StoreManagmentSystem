using StoreManagmentSystem.Data.Entities;

namespace StoreManagmentSystem.Repository
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task DeleteUser(User user); 
        Task UpdateUser(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(Guid Id);

        Task SaveChangesAsync();
    }
}
