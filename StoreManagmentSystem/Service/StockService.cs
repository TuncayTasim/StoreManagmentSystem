using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Enums;
using StoreManagmentSystem.Models.StockModels;
using StoreManagmentSystem.Models.UserModels;
using StoreManagmentSystem.Repository;

namespace StoreManagmentSystem.Service
{
    public class StockService:IStockService
    {
        private readonly IStockRepository _stockRepository;
        public StockService(IStockRepository inventoryRepository)
        {
            _stockRepository = inventoryRepository;
        }
        public async Task<IEnumerable<Stock>> GetAllStocksInInventory()
        {
            return await _stockRepository.GetAllStocksInInventory();
        }
        public async Task<Stock> GetStockById(int ProductId)
        {
            return await _stockRepository.GetStockById(ProductId);
        }
        public async Task AddStock(StockModel stock)
        {
           await _stockRepository.AddStock(stock);
        }
        public async Task<StockModel> UpdateStock(int ProductId, StockModelNoId stock)
        {
            var existingStock = await _stockRepository.GetStockById(ProductId);
            if (existingStock == null)
            {
                return null;
            }
            existingStock.QuantityRestocked = stock.QuantityRestocked;
            existingStock.PriceBought = stock.PriceBought;
            existingStock.DaysToExpire = stock.DaysToExpire;

            await _stockRepository.UpdateStock(existingStock);
            var stockModel = await _stockRepository.GetStockModelById(ProductId);
            return stockModel;
        }
        
        public async Task<Stock> DeleteStock(int StockId)
        {
            var existingStock = await _stockRepository.GetStockById(StockId);
            if (existingStock == null)
            {
                return null;
            }
            _stockRepository.DeleteStock(existingStock);
            return existingStock;
        }
    }
}
