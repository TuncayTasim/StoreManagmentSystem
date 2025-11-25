using StoreManagmentSystem.Data.Entities;

namespace StoreManagmentSystem.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
    }
}
