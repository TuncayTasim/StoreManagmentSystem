using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.StockModels;

namespace StoreManagmentSystem.Service
{
    public interface IWarehouseService
    {
        Task AddStock(WarehouseModel stock);
        Task<WarehouseRestock> DeleteStock(int StockId);
        Task<IEnumerable<WarehouseRestock>> GetAllStocksInInventory();
        Task<WarehouseRestock> GetStockById(int StockId);
        Task<decimal> GetStockCount(Guid productId);
        Task<WarehouseModel> UpdateStock(int StockId, WarehouseModelNoId stock);
    }
}