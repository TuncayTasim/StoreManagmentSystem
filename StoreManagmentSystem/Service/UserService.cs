using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Repository;

namespace StoreManagmentSystem.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }
    }
}
