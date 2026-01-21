using Microsoft.EntityFrameworkCore;
using StoreManagmentSystem.Data;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.ShelfModels;
using StoreManagmentSystem.Models.WarehouseModels;

namespace StoreManagmentSystem.Repository
{
    public class ShelfRepository: IShelfRepository
    {
        private readonly AppDbContext _context;
        public ShelfRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShelfRestock>> GetAllItemsOnShelf()
        {
            return await _context.ShelfRestocks
                .AsNoTracking()
                .Select(x => new ShelfRestock
            {
                ShelfId = x.ShelfId,
                ProductId = x.ProductId,
                PriceSell = x.PriceSell,
                QuantityRestocked = x.QuantityRestocked,
                RestockDate = x.RestockDate,
            }).ToListAsync();
        }
        public async Task<ShelfRestock> GetItemById(int shelfId)
        {
            return await _context.ShelfRestocks.FindAsync(shelfId);
        }
        public async Task AddItem(ShelfModel item)
        {
            var newItem = new ShelfRestock
            {
                ProductId = item.ProductId,
                PriceSell = item.PriceSell,
                QuantityRestocked = item.QuantityRestocked,
            };
            await _context.ShelfRestocks.AddAsync(newItem);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateItem(ShelfRestock item)
        {
            _context.ShelfRestocks.Update(item);
            await _context.SaveChangesAsync();
        }
        public async Task<ShelfDeleteResult> DeleteItem(ShelfRestock item)
        {
            var product = await _context.Products.FindAsync(item.ProductId);

            _context.ShelfRestocks.Remove(item);
            await _context.SaveChangesAsync();

            return new ShelfDeleteResult
            {
                Success = true,
                DeletedItem = item,
                QuantityInShelf = product.QuantityInShelf,
                RestockRecommended = product.QuantityInShelf <= 10,
                Message = product.QuantityInShelf <= 10
                    ? "Item deleted. Quantity is 10 or below. Please restock."
                    : "Item deleted successfully."
            };
        }

        public async Task<IEnumerable<ShelfRestock>> GetAllItemsByProductId(Guid ProductId)
        {
            return await _context.ShelfRestocks
           .AsNoTracking()
           .Where(u => u.ProductId == ProductId)
           .Select(u => new ShelfRestock
           {
               ShelfId = u.ShelfId,
               ProductId = u.ProductId,
               PriceSell = u.PriceSell,
               QuantityRestocked = u.QuantityRestocked,
               RestockDate = u.RestockDate
           })
           .ToListAsync();
        }

        public ShelfModel MapToModel(ShelfRestock item)
        {
            return new ShelfModel
            {
                ProductId = item.ProductId,
                PriceSell = item.PriceSell,
                QuantityRestocked = item.QuantityRestocked

            };
        }
        public async Task<ShelfModel> GetItemModelById(int shelfId)
        {
            var item = await _context.ShelfRestocks.FindAsync(shelfId);
            return MapToModel(item);
        }
    }
}
