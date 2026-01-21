using Microsoft.AspNetCore.Http.HttpResults;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.ShelfModels;
using StoreManagmentSystem.Models.WarehouseModels;
using StoreManagmentSystem.Repository;

namespace StoreManagmentSystem.Service
{
    public class ShelfService : IShelfService
    {
        private readonly IShelfRepository _shelfRepository;
        private readonly IProductRepository _productRepository;
        public ShelfService(IShelfRepository shelfRepository, IProductRepository productRepository)
        {
            _shelfRepository = shelfRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ShelfRestock>> GetAllItemsOnShelf()
        {
            return await _shelfRepository.GetAllItemsOnShelf();
        }

        public async Task<ShelfRestock> GetItemById(int shelfId)
        {
            return await _shelfRepository.GetItemById(shelfId);
        }
        public async Task AddItem(ShelfModel item)
        {
            var product = await _productRepository.GetProductById(item.ProductId);
            if (product != null)
            {
                product.QuantityInShelf += item.QuantityRestocked;
            }
            await _shelfRepository.AddItem(item);
        }
        public async Task<ShelfModel> UpdateItem(int shelfId, ShelfModelNoId item)
        {
            var existingItem = await _shelfRepository.GetItemById(shelfId);
            if (existingItem == null)
            {
                return null;
            }

            existingItem.PriceSell = item.PriceSell;
            existingItem.QuantityRestocked = item.QuantityRestocked;

            await _shelfRepository.UpdateItem(existingItem);
            var shelfModel = await _shelfRepository.GetItemModelById(shelfId);
            return shelfModel;
        }
        public async Task<ShelfDeleteResult> DeleteItem(int shelfId)
        {
            var existingItem = await _shelfRepository.GetItemById(shelfId);
            if (existingItem == null)
            {
                return new ShelfDeleteResult
                {
                    Success = false,
                    Message = "Product for this stock entry was not found."
                };
            }
            var product = await _productRepository.GetProductById(existingItem.ProductId);
            if (product.QuantityInShelf < existingItem.QuantityRestocked)
            {
                return new ShelfDeleteResult
                {
                    Success = false,
                    Message = "Operation is impossible to be done because of lack of availability."
                };
            }

            product.QuantityInShelf -= existingItem.QuantityRestocked;
            return await _shelfRepository.DeleteItem(existingItem);
        }

        public async Task<IEnumerable<ShelfRestock>> GetAllItemsByProductId(Guid ProductId)
        {
            var allStocks = await _shelfRepository.GetAllItemsByProductId(ProductId);
            return allStocks;
        }
    }
}
