using Microsoft.EntityFrameworkCore;
using StoreManagmentSystem.Data;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.SaleModels;
using StoreManagmentSystem.Models.ShelfModels;
using StoreManagmentSystem.Models.WarehouseModels;

namespace StoreManagmentSystem.Repository
{
    public class ShelfRepository : IShelfRepository
    {
        private readonly AppDbContext _context;
        public ShelfRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shelf>> GetAllItemsOnShelf()
        {
            return await _context.Shelf
                .AsNoTracking()
                .Select(x => new Shelf
                {
                    ShelfId = x.ShelfId,
                    ActionId = x.ActionId,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    ActionDate = x.ActionDate,
                    CurrentQuantity = x.CurrentQuantity
                }).ToListAsync();
        }
        public async Task<ShelfRestock> GetItemById(int itemId)
        {
            return await _context.ShelfRestocks.FindAsync(itemId);
        }
        public async Task<IEnumerable<ShelfRestock>> GetAllMoves()
        {
            return await _context.ShelfRestocks
                .AsNoTracking()
                .Select(x => new ShelfRestock
                {
                    ItemId = x.ItemId,
                    PriceSell = x.PriceSell,
                    ShelfId = x.ShelfId
                }).ToListAsync();
        }
        public async Task<Shelf> GetMoveRecordById(int shelfId)
        {
            return await _context.Shelf.FindAsync(shelfId);
        }
        public async Task MoveToShelf(ShelfModel item)
        {
            var newShelfRecord = new Shelf
            {
                ActionId = 3,
                ProductId = item.ProductId,
                Quantity = item.RestockCurrentQuantity,
                ActionDate = DateTime.UtcNow,
                CurrentQuantity = item.RestockCurrentQuantity
            };

            await _context.Shelf.AddAsync(newShelfRecord);
            await _context.SaveChangesAsync();

            var newItem = new ShelfRestock
            {
                PriceSell = item.PriceSell,
                ShelfId = newShelfRecord.ShelfId,
            };

            await _context.ShelfRestocks.AddAsync(newItem);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateItem(ShelfRestock item, Shelf shelfRecord)
        {
            _context.ShelfRestocks.Update(item);
            _context.Shelf.Update(shelfRecord);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<ShelfModel>> GetAllItemsByProductId(Guid ProductId)
        {
            return await _context.Shelf
            .AsNoTracking()
            .Join(
                _context.ShelfRestocks,
                sh => sh.ShelfId,
                shr => shr.ShelfId,
                (sh, shr) => new { sh, shr }
                )
             .Where(x => x.sh.ProductId == ProductId && x.sh.ActionId == 3)
            .Select(x => new ShelfModel
            {
                ProductId = x.sh.ProductId,
                PriceSell = x.shr.PriceSell,
                RestockCurrentQuantity = x.sh.CurrentQuantity,
                ActionDate = x.sh.ActionDate
            })
            .ToListAsync();
        }

        public ShelfModel MapToModel(ShelfRestock item, Shelf shelfRecord)
        {
            return new ShelfModel
            {
                ProductId = shelfRecord.ProductId,
                PriceSell = item.PriceSell,
                RestockCurrentQuantity = shelfRecord.CurrentQuantity,
                ActionDate = shelfRecord.ActionDate
            };
        }
        public async Task<ShelfModel> GetItemModelById(int itemId)
        {
            var item = await _context.ShelfRestocks.FindAsync(itemId);
            var itemRecord = await _context.Shelf.FindAsync(item.ShelfId);
            return MapToModel(item, itemRecord);
        }
        public async Task<ShelfRestock> GetItemEntityByModel(ShelfModel item)
        {
            var moveRecord = await GetMoveRecordByModel(item);
            var shelfItem = await _context.ShelfRestocks
                .FirstOrDefaultAsync(wr => wr.ShelfId == moveRecord.ShelfId);
            return shelfItem;
        }
        //
        public async Task<Shelf> GetMoveRecordByModel(ShelfModel item)
        {
            var moveRecord = await _context.Shelf
                .FirstOrDefaultAsync(w => w.ProductId == item.ProductId &&
        w.ActionDate >= item.ActionDate.AddSeconds(-1) &&
        w.ActionDate <= item.ActionDate.AddSeconds(1));
            return moveRecord;
        } 

        //public async Task<ShelfDeleteResult> DeleteItem(ShelfRestock item)
        //{
        //    var product = await _context.Products.FindAsync(item.ProductId);

        //    _context.ShelfRestocks.Remove(item);
        //    await _context.SaveChangesAsync();

        //    return new ShelfDeleteResult
        //    {
        //        Success = true,
        //        DeletedItem = item,
        //        QuantityInShelf = product.QuantityInShelf,
        //        RestockRecommended = product.QuantityInShelf <= 10,
        //        Message = product.QuantityInShelf <= 10
        //            ? "Item deleted. Quantity is 10 or below. Please restock."
        //            : "Item deleted successfully."
        //    };
        //}
    }
}
