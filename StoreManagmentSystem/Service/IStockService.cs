using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.StockModels;

namespace StoreManagmentSystem.Service
{
    public interface IStockService
    {
        Task AddStock(StockModel stock);
        Task<Stock> DeleteStock(int StockId);
        Task<IEnumerable<Stock>> GetAllStocksInInventory();
        Task<Stock> GetStockById(int StockId);
        Task<StockModel> UpdateStock(int StockId, StockModelNoId stock);
    }
}