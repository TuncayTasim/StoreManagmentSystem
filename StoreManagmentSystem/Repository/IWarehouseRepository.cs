using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.WarehouseModels;

namespace StoreManagmentSystem.Repository
{
    public interface IWarehouseRepository
    {
        Task AddStock(WarehouseModel stock);
        Task<WarehouseDeleteResult> DeleteStock(WarehouseRestock stock);
        Task<IEnumerable<WarehouseRestock>> GetAllStocksByProductId(Guid ProductId);
        Task<IEnumerable<WarehouseRestock>> GetAllStocksInInventory();
        Task<WarehouseRestock> GetStockById(int StockId);
        Task<WarehouseModel> GetStockModelById(int stockId);
        Task UpdateStock(WarehouseRestock stock);
    }
}