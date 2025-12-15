using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.UserModels;

namespace StoreManagmentSystem.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<UserModelWithPass> AddUser(UserModelWithPass user);
        Task<User> DeleteUser(Guid UserId);
        Task<UserModelWithRole> UpdateUser(Guid UserId, UserModelToChange newUserInfo);

        Task<UserModelWithRole> GetModelById(Guid UserId);

        Task<User> GetUserByEmail(string Email);
        Task<User> GetUserById(Guid Id);
        Task<User> GetUserByToken(string Token);

        Task<string> RequestPasswordReset(string email);
        Task<User> ResetPassword(User User, string newPassword);

        Task<User> UpdateUserPassword(User userToUpdate, string currentPass, string newUserPassword);
        Task<User> UpdateUserRole(Guid userId, string newRole);

        bool VerifyPassword(string password, string hashedPassword);
        string GetJWTToken(User User, string password); 
        string HashPassword(string password);
        Task<string> RequestEmailConfirm(string email);
        Task<User> ChangeStatus(User user, string newStatus);
    }
}
