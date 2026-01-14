using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.StockModels;

namespace StoreManagmentSystem.Repository
{
    public interface IStockRepository
    {
        Task AddStock(StockModel stock);
        Task DeleteStock(Stock stock);
        Task<IEnumerable<Stock>> GetAllStocksInInventory();
        Task<Stock> GetStockById(int StockId);
        Task<StockModel> GetStockModelById(int stockId);
        Task UpdateStock(Stock stock);
    }
}