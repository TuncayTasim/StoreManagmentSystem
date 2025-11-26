using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Repository;
using System;
using System.Threading.Tasks;

namespace StoreManagmentSystem.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> AddUser(User user)
        {
            await _userRepository.AddUser(user);
            return user;
        }

        public async Task<User> DeleteUser(Guid UserId)
        {
            var user = await _userRepository.GetUserById(UserId);

            if (user == null)
            {
                return null;
            }

            await _userRepository.DeleteUser(user);
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<User> GetUserById(Guid UserId)
        {
            return await _userRepository.GetUserById(UserId);
        }

        public async Task<User> UpdateUser(Guid UserId, User newUserInfo)
        {
            var user = await _userRepository.GetUserById(UserId);

            if (user == null)
            {
                return null;
            }

            user.UserName = newUserInfo.UserName;
            user.FirstName = newUserInfo.FirstName;
            user.LastName = newUserInfo.LastName;
            user.RoleId = newUserInfo.RoleId;
            user.Email = newUserInfo.Email;
            user.PhoneNumber = newUserInfo.PhoneNumber;
            user.Note = newUserInfo.Note;

            await _userRepository.UpdateUser(user);
            return user;
        }
    }
}
