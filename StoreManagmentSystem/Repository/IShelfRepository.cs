using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.SaleModels;
using StoreManagmentSystem.Models.ShelfModels;

namespace StoreManagmentSystem.Repository
{
    public interface IShelfRepository
    {
        Task<IEnumerable<ShelfModel>> GetAllItemsByProductId(Guid ProductId);
        Task<IEnumerable<Shelf>> GetAllItemsOnShelf();
        Task<IEnumerable<ShelfRestock>> GetAllMoves();
        Task<ShelfRestock> GetItemById(int shelfId);
        Task<ShelfRestock> GetItemEntityByModel(ShelfModel item);
        Task<ShelfModel> GetItemModelById(int itemId);
        Task<Shelf> GetMoveRecordById(int shelfId);
        Task<Shelf> GetMoveRecordByModel(ShelfModel item);
        ShelfModel MapToModel(ShelfRestock item, Shelf shelfRecord);
        Task MoveToShelf(ShelfModel item);
        Task UpdateItem(ShelfRestock item, Shelf shelfRecord);
    }
}