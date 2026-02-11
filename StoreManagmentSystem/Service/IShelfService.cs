using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.SaleModels;
using StoreManagmentSystem.Models.ShelfModels;

namespace StoreManagmentSystem.Service
{
    public interface IShelfService
    {
        Task<IEnumerable<Shelf>> GetAllItemsOnShelf();
        Task<IEnumerable<ShelfModel>> GetAllItemsByProductId(Guid ProductId);
        Task<ShelfRestock> GetItemById(int shelfId);
        Task<ShelfAddAndUpdateResult> MoveToShelf(ShelfModel item);
        Task<ShelfAddAndUpdateResult> UpdateItem(int shelfId, ShelfModelNoId item);
        Task<IEnumerable<ShelfRestock>> GetAllMoves();
        Task<Shelf> GetMoveRecordById(int shelfId);
        Task<(bool Success, decimal Available)> TryMoveFromWarehouse(Guid productId, decimal quantityToMove);
    }
}