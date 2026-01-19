using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Enums;
using StoreManagmentSystem.Models.StockModels;
using StoreManagmentSystem.Models.UserModels;
using StoreManagmentSystem.Repository;

namespace StoreManagmentSystem.Service
{
    public class WarehouseService:IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        public WarehouseService(IWarehouseRepository inventoryRepository)
        {
            _warehouseRepository = inventoryRepository;
        }
        public async Task<IEnumerable<WarehouseRestock>> GetAllStocksInInventory()
        {
            return await _warehouseRepository.GetAllStocksInInventory();
        }
        public async Task<WarehouseRestock> GetStockById(int ProductId)
        {
            return await _warehouseRepository.GetStockById(ProductId);
        }
        public async Task AddStock(WarehouseModel stock)
        {
           await _warehouseRepository.AddStock(stock);

        }
        public async Task<WarehouseModel> UpdateStock(int ProductId, WarehouseModelNoId stock)
        {
            var existingStock = await _warehouseRepository.GetStockById(ProductId);
            if (existingStock == null)
            {
                return null;
            }
            existingStock.QuantityRestocked = stock.QuantityRestocked;
            existingStock.PriceBought = stock.PriceBought;
            existingStock.DaysToExpire = stock.DaysToExpire;

            await _warehouseRepository.UpdateStock(existingStock);
            var stockModel = await _warehouseRepository.GetStockModelById(ProductId);
            return stockModel;
        }
        
        public async Task<WarehouseRestock> DeleteStock(int StockId)
        {
            var existingStock = await _warehouseRepository.GetStockById(StockId);
            if (existingStock == null)
            {
                return null;
            }
            _warehouseRepository.DeleteStock(existingStock);
            return existingStock;
        }
        public async Task<decimal> GetStockCount(Guid productId)
        {
            var stockList = await _warehouseRepository.GetStockCount(productId);
            if (stockList == null || !stockList.Any())
            {
                return -1;
            }
            decimal totalQuantity = stockList.Sum(s => s.QuantityRestocked);
            return totalQuantity;

        }
    }
}
