using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.ShelfModels;

namespace StoreManagmentSystem.Repository
{
    public interface IShelfRepository
    {
        Task AddItem(ShelfModel item);
        Task<ShelfDeleteResult> DeleteItem(ShelfRestock item);
        Task<IEnumerable<ShelfRestock>> GetAllItemsByProductId(Guid ProductId);
        Task<IEnumerable<ShelfRestock>> GetAllItemsOnShelf();
        Task<ShelfRestock> GetItemById(int shelfId);
        Task<ShelfModel> GetItemModelById(int shelfId);
        ShelfModel MapToModel(ShelfRestock item);
        Task UpdateItem(ShelfRestock item);
    }
}