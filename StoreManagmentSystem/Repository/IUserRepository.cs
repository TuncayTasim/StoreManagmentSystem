using StoreManagmentSystem.Data.Entities;

namespace StoreManagmentSystem.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
    }
}
