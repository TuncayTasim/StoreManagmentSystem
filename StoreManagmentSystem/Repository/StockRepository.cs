using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StoreManagmentSystem.Data;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.StockModels;
using StoreManagmentSystem.Models.UserModels;

namespace StoreManagmentSystem.Repository
{
    public class StockRepository:IStockRepository
    {
        private readonly AppDbContext _context;
        public StockRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Stock>> GetAllStocksInInventory()
        {
            return await _context.Stocks
           .AsNoTracking()
           .Select(u => new Stock
           {
               StockId = u.StockId,
               ProductId = u.ProductId,
               PriceBought = u.PriceBought,
               QuantityRestocked = u.QuantityRestocked,
               DaysToExpire = u.DaysToExpire,
               RestockDate = u.RestockDate
           })
           .ToListAsync();
        }
        public async Task<Stock> GetStockById(int StockId)
        {
            return await _context.Stocks.FindAsync(StockId);
        }
        public async Task AddStock(StockModel stock)
        {
            var newStock = new Stock
            {
                ProductId = stock.ProductId,
                PriceBought = stock.PriceBought,
                QuantityRestocked = stock.QuantityRestocked,
                DaysToExpire = stock.DaysToExpire
            };
            await _context.Stocks.AddAsync(newStock);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateStock(Stock stock)
        {
            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStock(Stock stock)
        {
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
        }

        public async Task<StockModel> GetStockModelById(int stockId)
        {
            var stock = await _context.Stocks.FindAsync(stockId);
            return MapToModel(stock);
        }
        public StockModel MapToModel(Stock stock)
        {
            return new StockModel
            {
                ProductId=stock.ProductId,
                PriceBought=stock.PriceBought,
                QuantityRestocked = stock.QuantityRestocked,
                DaysToExpire = stock.DaysToExpire

            };
        }
    }
}
