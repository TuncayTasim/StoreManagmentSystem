using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.WarehouseModels;
using StoreManagmentSystem.Repository;

namespace StoreManagmentSystem.Service
{
    public class WarehouseService:IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IProductRepository _productRepository;
        public WarehouseService(IWarehouseRepository inventoryRepository, IProductRepository productRepository)
        {
            _warehouseRepository = inventoryRepository;
            _productRepository = productRepository;
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
            var product = await _productRepository.GetProductById(stock.ProductId);
            if (product != null)
            {
                product.QuantityInWarehouse += stock.QuantityRestocked;
            }
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
        
        public async Task<WarehouseDeleteResult> DeleteStock(int StockId)
        {
            var existingStock = await _warehouseRepository.GetStockById(StockId);
            if (existingStock == null)
            {
                return new WarehouseDeleteResult
                {
                    Success = false,
                    Message = "Product for this stock entry was not found."
                };
            }
            var product = await _productRepository.GetProductById(existingStock.ProductId);
            if (product.QuantityInWarehouse < existingStock.QuantityRestocked)
            {
                return new WarehouseDeleteResult
                {
                    Success = false,
                    Message = "Operation is impossible to be done because of lack of availability."
                };
            }

            product.QuantityInWarehouse -= existingStock.QuantityRestocked;
            return await _warehouseRepository.DeleteStock(existingStock);
        }

        public async Task<IEnumerable<WarehouseRestock>> GetAllStocksByProductId(Guid ProductId)
        {
            var allStocks = await _warehouseRepository.GetAllStocksByProductId(ProductId);
            return allStocks;
        }
    }
}
