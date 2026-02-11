using Microsoft.AspNetCore.Http.HttpResults;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.SaleModels;
using StoreManagmentSystem.Models.ShelfModels;
using StoreManagmentSystem.Models.WarehouseModels;
using StoreManagmentSystem.Repository;

namespace StoreManagmentSystem.Service
{
    public class ShelfService : IShelfService
    {
        private readonly IShelfRepository _shelfRepository;
        private readonly IProductRepository _productRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        public ShelfService(IShelfRepository shelfRepository, IProductRepository productRepository, IWarehouseRepository warehouseRepository)
        {
            _shelfRepository = shelfRepository;
            _productRepository = productRepository;
            _warehouseRepository = warehouseRepository;
        }

        public async Task<IEnumerable<Shelf>> GetAllItemsOnShelf()
        {
            return await _shelfRepository.GetAllItemsOnShelf();
        }

        public async Task<ShelfRestock> GetItemById(int itemId)
        {
            return await _shelfRepository.GetItemById(itemId);
        }
        public async Task<IEnumerable<ShelfRestock>> GetAllMoves()
        {
            return await _shelfRepository.GetAllMoves();
        }

        public async Task<Shelf> GetMoveRecordById(int shelfId)
        {
            return await _shelfRepository.GetMoveRecordById(shelfId);
        }
        public async Task<(bool Success, decimal Available)> TryMoveFromWarehouse(Guid productId, decimal quantityToMove)
        {
            var warehouseRestocks = await _warehouseRepository.GetAllRestocksByProductId(productId);
            var ordered = warehouseRestocks.Where(stock => stock.RestockCurrentQuantity > 0).OrderBy(stock => stock.ActionDate).ToList();
            var totalAvailable = ordered.Sum(stock => stock.RestockCurrentQuantity);
            if (totalAvailable < quantityToMove)
            {
                return (false, totalAvailable);
            }

            decimal remaining = quantityToMove;

            foreach (var stock in ordered)
            {
                if (remaining == 0)
                {
                    break;
                }

                if (stock.RestockCurrentQuantity >= remaining)
                {
                    stock.RestockCurrentQuantity -= Math.Abs(remaining);
                    remaining = 0;
                }
                else if (stock.RestockCurrentQuantity < remaining)
                {
                    remaining -= stock.RestockCurrentQuantity;
                    stock.RestockCurrentQuantity = 0;
                }
                if (stock == null)
                {
                    throw new InvalidOperationException($"Cannot move item. Product {productId} not found in stock info.");
                }
                var warehouseRecord = await _warehouseRepository.GetWarehouseRecordByModel(stock);
                var warehouseStock = await _warehouseRepository.GetStockEntityByModel(stock);


                warehouseRecord.CurrentQuantity = stock.RestockCurrentQuantity;

                await _warehouseRepository.UpdateRestock(warehouseStock, warehouseRecord);
            }

            return (true, totalAvailable);
        }
        public async Task<ShelfAddAndUpdateResult> MoveToShelf(ShelfModel item)
        {
            if (item.RestockCurrentQuantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(item.RestockCurrentQuantity), "Quantity cannot be negative or zero.");
            }
            var product = await _productRepository.GetProductById(item.ProductId);
            if (product == null)
            {
                return new ShelfAddAndUpdateResult
                {
                    Success = false,
                    Message = "Product was not found."
                };
            }

            var moveResult = await TryMoveFromWarehouse(item.ProductId, item.RestockCurrentQuantity);
            if (!moveResult.Success)
            {
                return new ShelfAddAndUpdateResult
                {
                    Success = false,
                    Message = $"Not enough stock in warehouse. Available: {moveResult.Available}.",
                    QuantityInWarehouse = product.QuantityInWarehouse,
                    QuantityInShelf = product.QuantityInShelf
                };
            }

            product.QuantityInWarehouse -= item.RestockCurrentQuantity;
            product.QuantityInShelf += item.RestockCurrentQuantity;
            await _productRepository.UpdateProduct(product);

            await _shelfRepository.MoveToShelf(item);

            return new ShelfAddAndUpdateResult
            {
                Success = true,
                Message = "Item added to shelf successfully.",
                QuantityInWarehouse = product.QuantityInWarehouse,
                QuantityInShelf = product.QuantityInShelf
            };
        }
        public async Task<ShelfAddAndUpdateResult> UpdateItem(int itemId, ShelfModelNoId item)
        {
            if (item.RestockCurrentQuantity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(item.RestockCurrentQuantity), "Quantity cannot be negative.");
            }

            var existingItem = await _shelfRepository.GetItemModelById(itemId);
            if (existingItem == null)
            {
                return new ShelfAddAndUpdateResult
                {
                    Success = false,
                    Message = $"Item with ID {itemId} was not found."
                };
            }

            var product = await _productRepository.GetProductById(existingItem.ProductId);

            var difference = item.RestockCurrentQuantity - existingItem.RestockCurrentQuantity;

            if (difference > 0)
            {
                var moveResult = await TryMoveFromWarehouse(existingItem.ProductId, difference);
                if (!moveResult.Success)
                {
                    return new ShelfAddAndUpdateResult
                    {
                        Success = false,
                        Message = $"Not enough stock in warehouse. Available: {moveResult.Available}.",
                        QuantityInWarehouse = product.QuantityInWarehouse,
                        QuantityInShelf = product.QuantityInShelf
                    };
                }

                product.QuantityInWarehouse -= difference;
                product.QuantityInShelf += difference;
            }
            //
            else if (difference < 0)
            {
                product.QuantityInWarehouse += -difference;
                product.QuantityInShelf += difference;
            }

            await _productRepository.UpdateProduct(product);

            var shelfRecord = await _shelfRepository.GetMoveRecordByModel(existingItem);
            var shelfItem = await _shelfRepository.GetItemEntityByModel(existingItem);

            shelfItem.PriceSell = item.PriceSell;
            shelfRecord.CurrentQuantity = item.RestockCurrentQuantity;

            await _shelfRepository.UpdateItem(shelfItem, shelfRecord);

            return new ShelfAddAndUpdateResult
            {
                Success = true,
                Message = "Shelf item updated successfully.",
                QuantityInWarehouse = product.QuantityInWarehouse,
                QuantityInShelf = product.QuantityInShelf,
                ShelfItem = await _shelfRepository.GetItemEntityByModel(existingItem)
            };
        }
        public async Task<IEnumerable<ShelfModel>> GetAllItemsByProductId(Guid ProductId)
        {
            var allStocks = await _shelfRepository.GetAllItemsByProductId(ProductId);
            return allStocks;
        } 
        
        //public async Task<ShelfDeleteResult> DeleteItem(int shelfId)
        //{
        //    var existingItem = await _shelfRepository.GetItemById(shelfId);
        //    if (existingItem == null)
        //    {
        //        return new ShelfDeleteResult
        //        {
        //            Success = false,
        //            Message = "Product for this stock entry was not found."
        //        };
        //    }
        //    var product = await _productRepository.GetProductById(existingItem.ProductId);
        //    if (product.QuantityInShelf < existingItem.QuantityRestocked)
        //    {
        //        return new ShelfDeleteResult
        //        {
        //            Success = false,
        //            Message = "Operation is impossible to be done because of lack of availability."
        //        };
        //    }

        //    product.QuantityInShelf -= existingItem.QuantityRestocked;
        //    return await _shelfRepository.DeleteItem(existingItem);
        //}

    }
}
