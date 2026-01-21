using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StoreManagmentSystem.Data;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.WarehouseModels;
using StoreManagmentSystem.Models.UserModels;

namespace StoreManagmentSystem.Repository
{
    public class WarehouseRepository:IWarehouseRepository
    {
        private readonly AppDbContext _context;
        public WarehouseRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<WarehouseRestock>> GetAllStocksInInventory()
        {
            return await _context.WarehouseRestocks
           .AsNoTracking()
           .Select(u => new WarehouseRestock
           {
               WarehouseId = u.WarehouseId,
               ProductId = u.ProductId,
               PriceBought = u.PriceBought,
               QuantityRestocked = u.QuantityRestocked,
               DaysToExpire = u.DaysToExpire,
               RestockDate = u.RestockDate
           })
           .ToListAsync();
        }
        public async Task<WarehouseRestock> GetStockById(int StockId)
        {
            return await _context.WarehouseRestocks.FindAsync(StockId);
        }
        public async Task AddStock(WarehouseModel stock)
        {
            var newStock = new WarehouseRestock
            {
                ProductId = stock.ProductId,
                PriceBought = stock.PriceBought,
                QuantityRestocked = stock.QuantityRestocked,
                DaysToExpire = stock.DaysToExpire
            };
            await _context.WarehouseRestocks.AddAsync(newStock);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateStock(WarehouseRestock stock)
        {
            _context.WarehouseRestocks.Update(stock);
            await _context.SaveChangesAsync();
        }

        public async Task<WarehouseDeleteResult> DeleteStock(WarehouseRestock stock)
        {
            var product = await _context.Products.FindAsync(stock.ProductId);

            _context.WarehouseRestocks.Remove(stock);
            await _context.SaveChangesAsync();

            return new WarehouseDeleteResult
            {
                Success = true,
                DeletedStock = stock,
                QuantityInWarehouse = product.QuantityInWarehouse,
                RestockRecommended = product.QuantityInWarehouse <= 10,
                Message = product.QuantityInWarehouse <= 10
                    ? "Stock deleted. Quantity is 10 or below. Please restock."
                    : "Stock deleted successfully."
            };
        }

        public async Task<WarehouseModel> GetStockModelById(int stockId)
        {
            var stock = await _context.WarehouseRestocks.FindAsync(stockId);
            return MapToModel(stock);
        }
        public WarehouseModel MapToModel(WarehouseRestock stock)
        {
            return new WarehouseModel
            {
                ProductId=stock.ProductId,
                PriceBought=stock.PriceBought,
                QuantityRestocked = stock.QuantityRestocked,
                DaysToExpire = stock.DaysToExpire

            };
        }

        public async Task<IEnumerable<WarehouseRestock>> GetAllStocksByProductId(Guid ProductId)
        {
            return await _context.WarehouseRestocks
           .AsNoTracking()
           .Where(u => u.ProductId == ProductId)
           .Select(u => new WarehouseRestock
           {
               WarehouseId = u.WarehouseId,
               ProductId = u.ProductId,
               PriceBought = u.PriceBought,
               QuantityRestocked = u.QuantityRestocked,
               DaysToExpire = u.DaysToExpire,
               RestockDate = u.RestockDate
           })
           .ToListAsync();
        }
    }
}
