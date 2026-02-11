using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.WarehouseModels;
using StoreManagmentSystem.Repository;

namespace StoreManagmentSystem.Service
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IProductRepository _productRepository;
        public WarehouseService(IWarehouseRepository inventoryRepository, IProductRepository productRepository)
        {
            _warehouseRepository = inventoryRepository;
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<WarehouseRestock>> GetAllRestocks()
        {
            return await _warehouseRepository.GetAllRestocks();
        }
        public async Task<WarehouseRestock> GetRestockById(int stockId)
        {
            return await _warehouseRepository.GetRestockById(stockId);
        }
        public async Task<IEnumerable<Warehouse>> GetAllStocksInWarehouse()
        {
            return await _warehouseRepository.GetAllStocksInWarehouse();
        }
        public async Task<Warehouse> GetRestockRecordById(int warehouseId)
        {
            return await _warehouseRepository.GetRestockRecordById(warehouseId);
        }
        public async Task<IEnumerable<WarehouseModel>> GetAllRestocksByProductId(Guid ProductId)
        {
            var allStocks = await _warehouseRepository.GetAllRestocksByProductId(ProductId);
            return allStocks;
        }
        public async Task Restock(WarehouseModel stock)
        {
            if (stock.RestockCurrentQuantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(stock.RestockCurrentQuantity), "Quantity cannot be negative or zero.");
            }

            var product = await _productRepository.GetProductById(stock.ProductId);
            if (product != null)
            {
                product.QuantityInWarehouse += stock.RestockCurrentQuantity;
            }
            await _warehouseRepository.Restock(stock);

        }
        public async Task<WarehouseModel> UpdateRestock(int stockId, WarehouseModelNoId updatedStock)
        {
            if (updatedStock.RestockCurrentQuantity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(updatedStock.RestockCurrentQuantity), "Quantity cannot be negative.");
            }

            var existingStock = await _warehouseRepository.GetRestockById(stockId);
            if (existingStock == null)
            {
                return null;
            }
            var restockRecord = await _warehouseRepository.GetRestockRecordById(existingStock.WarehouseId);
            var quantityDelta = updatedStock.RestockCurrentQuantity - restockRecord.Quantity;
            if (quantityDelta != 0)
            {
                var product = await _productRepository.GetProductById(restockRecord.ProductId);
                if (product != null)
                {
                    product.QuantityInWarehouse += quantityDelta;
                    await _productRepository.UpdateProduct(product);
                }
            }
            restockRecord.Quantity = updatedStock.RestockCurrentQuantity;
            restockRecord.CurrentQuantity = updatedStock.RestockCurrentQuantity;
            existingStock.PriceBought = updatedStock.PriceBought;
            existingStock.DaysToExpire = updatedStock.DaysToExpire;

            await _warehouseRepository.UpdateRestock(existingStock, restockRecord);
            var stockModel = await _warehouseRepository.GetRestockModelById(stockId);
            return stockModel;
        }
       

        // the methods below will be changed
        //public async Task<WarehouseDeleteResult> DeleteStock(int StockId)
        //{
        //    var existingStock = await _warehouseRepository.GetRestockById(StockId);
        //    if (existingStock == null)
        //    {
        //        return new WarehouseDeleteResult
        //        {
        //            Success = false,
        //            Message = "Product for this stock entry was not found."
        //        };
        //    }
        //    var product = await _productRepository.GetProductById(existingStock.ProductId);
        //    if (product.QuantityInWarehouse < existingStock.QuantityRestocked)
        //    {
        //        return new WarehouseDeleteResult
        //        {
        //            Success = false,
        //            Message = "Operation is impossible to be done because of lack of availability."
        //        };
        //    }

        //    product.QuantityInWarehouse -= existingStock.QuantityRestocked;
        //    return await _warehouseRepository.DeleteStock(existingStock);
        //}


        //public async Task<IEnumerable<WarehouseRestock>> RemoveExpiredStocks()
        //{
        //    var removedStocks = await _warehouseRepository.RemoveExpiredStocks();
        //    if (removedStocks == null)
        //    {
        //        return null;
        //    }
        //    foreach (var stock in removedStocks)
        //    {
        //        var product = await _productRepository.GetProductById(stock.ProductId);

        //        product.QuantityInWarehouse -= stock.QuantityRestocked;
        //        await _productRepository.UpdateProduct(product);
        //    }

        //    return removedStocks;
        //}
    }
}
