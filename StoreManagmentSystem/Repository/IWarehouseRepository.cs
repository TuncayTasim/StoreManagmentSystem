using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.StockModels;

namespace StoreManagmentSystem.Repository
{
    public interface IWarehouseRepository
    {
        Task AddStock(WarehouseModel stock);
        Task DeleteStock(WarehouseRestock stock);
        Task<IEnumerable<WarehouseRestock>> GetAllStocksInInventory();
        Task<WarehouseRestock> GetStockById(int StockId);
        Task<List<WarehouseRestock>> GetStockCount(Guid productId);
        Task<WarehouseModel> GetStockModelById(int stockId);
        Task UpdateStock(WarehouseRestock stock);
    }
}