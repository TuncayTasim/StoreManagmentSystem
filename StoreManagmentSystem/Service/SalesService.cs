using StoreManagmentSystem.Models.SaleModels;
using StoreManagmentSystem.Models.ShelfModels;
using StoreManagmentSystem.Repository;
using System.Threading.Tasks.Dataflow;

namespace StoreManagmentSystem.Service
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IShelfRepository _shelfRepository;
        private readonly IProductRepository _productRepository;

        public SalesService(ISalesRepository salesRepository, IShelfRepository shelfRepository, IProductRepository productRepository)
        {
            _salesRepository = salesRepository;
            _shelfRepository = shelfRepository;
            _productRepository = productRepository;
        }

        public async Task<(bool Success, decimal Remaining, ShelfModel item)> TrySell(Guid productId, decimal quantityToSell)
        {
            var shelfItems = await _shelfRepository.GetAllItemsByProductId(productId);
            var ordered = shelfItems.Where(item => item.RestockCurrentQuantity > 0).OrderBy(item => item.ActionDate).ToList();
            var totalAvailable = ordered.Sum(item => item.RestockCurrentQuantity);
            if (totalAvailable < quantityToSell)
            {
                return (false, totalAvailable, null);
            }

            decimal remaining = quantityToSell;

            var item = ordered[0];

            if (item.RestockCurrentQuantity >= remaining)
            {
                item.RestockCurrentQuantity -= Math.Abs(remaining);
                remaining = 0;
            }
            else if (item.RestockCurrentQuantity < remaining)
            {
                remaining -= item.RestockCurrentQuantity;
                item.RestockCurrentQuantity = 0;
            }
            if (item == null)
            {
                throw new InvalidOperationException($"Cannot move item. Product {productId} not found in stock info.");
            }
            var shelfRecord = await _shelfRepository.GetMoveRecordByModel(item);
            var shelfItem = await _shelfRepository.GetItemEntityByModel(item);

            shelfRecord.CurrentQuantity = item.RestockCurrentQuantity;

            await _shelfRepository.UpdateItem(shelfItem, shelfRecord);


            return (true, remaining, item);
        }

        public async Task<SaleResult> SellItem(SaleModel sale)
        {
            if (sale.QuantitySell <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sale.QuantitySell), "Quantity cannot be negative or zero.");
            }
            var product = await _productRepository.GetProductById(sale.ProductId);
            if (product == null)
            {
                return new SaleResult
                {
                    Success = false,
                    Message = "Product was not found."
                };
            }

            decimal toSell = sale.QuantitySell;
            bool succeeded = false;
            while (toSell > 0)
            {
                var moveResult = await TrySell(sale.ProductId, toSell);
                if (moveResult.Success == false)
                {
                    succeeded = false;
                    break;
                }
                decimal soldQuantity = toSell - moveResult.Remaining;
                await _salesRepository.SellItem(product, soldQuantity, moveResult.item);
                if (moveResult.Remaining == 0)
                {
                    toSell = 0;
                    succeeded = true;
                    break;
                }
                toSell = moveResult.Remaining;
                succeeded = true;
            }
            if (succeeded == false)
            {
                return new SaleResult
                {
                    Success = false,
                    Message = $"Not enough stock in shelf. Available: {product.QuantityInShelf}.",
                    QuantityInShelf = product.QuantityInShelf
                };
            }

            product.QuantityInShelf -= sale.QuantitySell;
            await _productRepository.UpdateProduct(product);

            return new SaleResult
            {
                Success = true,
                QuantityInShelf = product.QuantityInShelf,
                RestockRecommended = product.QuantityInShelf <= 10,
                Message = product.QuantityInShelf <= 10
                    ? "Item sold. Quantity is 10 or below. Please restock."
                    : "Item sold successfully."
            };
        }
    }
}
