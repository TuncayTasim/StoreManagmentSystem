using StoreManagmentSystem.Data.Entities;

namespace StoreManagmentSystem.Service
{
    public interface IUserService
    {
        Task<User> AddUser(User user);
        Task<User> DeleteUser(Guid UserId);
        Task<IEnumerable<User>> GetAllUsers();
        string GetToken(User User, string password);
        Task<User> GetUserByEmail(string Email);
        Task<User> GetUserById(Guid Id);
        string HashPassword(string password);
        Task<User> UpdateUser(Guid UserId, User newUserInfo);
        Task<User> UpdateUserPassword(User userToUpdate, string currentPass, string newUserPassword);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
