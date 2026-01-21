using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.WarehouseModels;

namespace StoreManagmentSystem.Service
{
    public interface IWarehouseService
    {
        Task AddStock(WarehouseModel stock);
        Task<WarehouseDeleteResult> DeleteStock(int StockId);
        Task<IEnumerable<WarehouseRestock>> GetAllStocksByProductId(Guid ProductId);
        Task<IEnumerable<WarehouseRestock>> GetAllStocksInInventory();
        Task<WarehouseRestock> GetStockById(int StockId);
        Task<WarehouseModel> UpdateStock(int StockId, WarehouseModelNoId stock);
    }
}