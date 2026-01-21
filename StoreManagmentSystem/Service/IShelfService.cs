using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.ShelfModels;

namespace StoreManagmentSystem.Service
{
    public interface IShelfService
    {
        Task AddItem(ShelfModel item);
        Task<ShelfDeleteResult> DeleteItem(int shelfId);
        Task<IEnumerable<ShelfRestock>> GetAllItemsOnShelf();
        Task<IEnumerable<ShelfRestock>> GetAllItemsByProductId(Guid ProductId);
        Task<ShelfRestock> GetItemById(int shelfId);
        Task<ShelfModel> UpdateItem(int shelfId, ShelfModelNoId item);
    }
}