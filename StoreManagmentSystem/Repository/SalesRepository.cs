using StoreManagmentSystem.Data;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.SaleModels;
using StoreManagmentSystem.Models.ShelfModels;

namespace StoreManagmentSystem.Repository
{
    public class SalesRepository: ISalesRepository
    {
        private readonly AppDbContext _context;

        public SalesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SaleResult> SellItem(Product product,decimal soldQuantity, ShelfModel item)
        {
            var newShelfRecord = new Shelf
            {
                ActionId = 5,
                ProductId = item.ProductId,
                Quantity = soldQuantity,
                ActionDate = DateTime.UtcNow,
                CurrentQuantity = soldQuantity
            };

            await _context.Shelf.AddAsync(newShelfRecord);
            await _context.SaveChangesAsync();

            var newItem = new Sale
            {
                PriceSold = item.PriceSell,
                ShelfId = newShelfRecord.ShelfId,
                QuantitySold = soldQuantity
            };

            await _context.Sales.AddAsync(newItem);
            await _context.SaveChangesAsync();

            return new SaleResult
            {
                Success = true,
                QuantityInShelf = product.QuantityInShelf
            };
        }
    }
}
