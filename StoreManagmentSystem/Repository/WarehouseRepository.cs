using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StoreManagmentSystem.Data;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.WarehouseModels;
using StoreManagmentSystem.Models.UserModels;

namespace StoreManagmentSystem.Repository
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly AppDbContext _context;
        public WarehouseRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<WarehouseRestock>> GetAllRestocks()
        {
            return await _context.WarehouseRestocks
           .AsNoTracking()
           .Select(u => new WarehouseRestock
           {
               StockId = u.StockId,
               PriceBought = u.PriceBought,
               DaysToExpire = u.DaysToExpire,
               WarehouseId = u.WarehouseId
           })
           .ToListAsync();
        }
        public async Task<IEnumerable<Warehouse>> GetAllStocksInWarehouse()
        {
            return await _context.Warehouse
           .AsNoTracking()
           .Select(u => new Warehouse
           {
                WarehouseId = u.WarehouseId,
                ActionId = u.ActionId,
                ProductId = u.ProductId,
                Quantity = u.Quantity,
                ActionDate = u.ActionDate,
                CurrentQuantity = u.CurrentQuantity
           })
           .ToListAsync();
        }
        public async Task<WarehouseRestock> GetRestockById(int stockId)
        {
            return await _context.WarehouseRestocks.FindAsync(stockId);
        }
        public async Task<Warehouse> GetRestockRecordById(int warehouseId)
        {
            return await _context.Warehouse.FindAsync(warehouseId);
        }
        public async Task Restock(WarehouseModel stock)
        {
            var newWarehouseRecord = new Warehouse
            {
                ActionId = 1,
                ProductId = stock.ProductId,
                Quantity = stock.RestockCurrentQuantity,
                ActionDate = DateTime.UtcNow,
                CurrentQuantity =stock.RestockCurrentQuantity
            };

            await _context.Warehouse.AddAsync(newWarehouseRecord);
            await _context.SaveChangesAsync();

            var newStock = new WarehouseRestock
            {
                PriceBought = stock.PriceBought,
                DaysToExpire = stock.DaysToExpire,
                WarehouseId = newWarehouseRecord.WarehouseId,
            }; 

            await _context.WarehouseRestocks.AddAsync(newStock);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateRestock(WarehouseRestock stock, Warehouse warehouseRecord)
        {
            _context.WarehouseRestocks.Update(stock);
            _context.Warehouse.Update(warehouseRecord);
            await _context.SaveChangesAsync();
        }
        public async Task<WarehouseModel> GetRestockModelById(int stockId)
        {
            var stock = await _context.WarehouseRestocks.FindAsync(stockId);
            var restockRecord = await _context.Warehouse.FindAsync(stock.WarehouseId);
            return MapToModel(stock, restockRecord);
        }
        public WarehouseModel MapToModel(WarehouseRestock stock, Warehouse restockRecord)
        {
            return new WarehouseModel
            {
                ProductId = restockRecord.ProductId,
                PriceBought = stock.PriceBought,
                RestockCurrentQuantity = restockRecord.CurrentQuantity,
                DaysToExpire = stock.DaysToExpire,
                ActionDate = restockRecord.ActionDate
            };
        }
        public async Task<IEnumerable<WarehouseModel>> GetAllRestocksByProductId(Guid ProductId)
        {
            return await _context.Warehouse
            .AsNoTracking()
            .Join(
                _context.WarehouseRestocks,
                w => w.WarehouseId,
                wr => wr.WarehouseId,
                (w, wr) => new { w, wr }
                )
             .Where(x => x.w.ProductId == ProductId)
            .Select(x => new WarehouseModel
            {
                ProductId = x.w.ProductId,
                PriceBought = x.wr.PriceBought,
                RestockCurrentQuantity = x.w.CurrentQuantity,
                DaysToExpire = x.wr.DaysToExpire,
                ActionDate = x.w.ActionDate,
            })
            .ToListAsync();
        }
        public async Task<WarehouseRestock> GetStockEntityByModel(WarehouseModel stock)
        {
            var warehouseRecord = await GetWarehouseRecordByModel(stock);
            var warehouseStock = await _context.WarehouseRestocks
                .FirstOrDefaultAsync(wr => wr.WarehouseId == warehouseRecord.WarehouseId);
            return warehouseStock;
        }
        //
        public async Task<Warehouse> GetWarehouseRecordByModel(WarehouseModel stock)
        {
            var warehouseRecord = await _context.Warehouse
                .FirstOrDefaultAsync(w => w.ProductId == stock.ProductId &&
        w.ActionDate >= stock.ActionDate.AddSeconds(-1) &&
        w.ActionDate <= stock.ActionDate.AddSeconds(1));
            return warehouseRecord;
        }

        //this will be moved after implementing new table for rejections
        //public async Task<WarehouseDeleteResult> DeleteStock(WarehouseRestock stock)
        //{
        //    var record = await _context.Warehouse.FindAsync(stock.WarehouseId);
        //    var product = await _context.Products.FindAsync(record.ProductId);

        //    _context.WarehouseRestocks.Remove(stock);
        //    await _context.SaveChangesAsync();

        //    return new WarehouseDeleteResult
        //    {
        //        Success = true,
        //        DeletedStock = stock,
        //        QuantityInWarehouse = product.QuantityInWarehouse,
        //        RestockRecommended = product.QuantityInWarehouse <= 10,
        //        Message = product.QuantityInWarehouse <= 10
        //            ? "Stock deleted. Quantity is 10 or below. Please restock."
        //            : "Stock deleted successfully."
        //    };
        //}
        //public async Task<IEnumerable<WarehouseRestock>> RemoveExpiredStocks()
        //{
        //    var today = DateTime.UtcNow;
        //    var expiredStocks = await _context.WarehouseRestocks
        //        .Where(s => s.RestockDate.AddDays(s.DaysToExpire) < today)
        //        .ToListAsync();
        //    _context.WarehouseRestocks.RemoveRange(expiredStocks);
        //    await _context.SaveChangesAsync();
        //    return expiredStocks;
        //}
    }
}
