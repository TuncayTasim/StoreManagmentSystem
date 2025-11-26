using StoreManagmentSystem.Data.Entities;

namespace StoreManagmentSystem.Service
{
    public interface IUserService
    {
        Task<User> AddUser(User user);
        Task<User> DeleteUser(Guid UserId);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(Guid Id);
        Task<User> UpdateUser(Guid UserId, User newUserInfo);
    }
}
