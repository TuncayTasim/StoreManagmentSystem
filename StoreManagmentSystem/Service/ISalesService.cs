using StoreManagmentSystem.Models.SaleModels;
using StoreManagmentSystem.Models.ShelfModels;

namespace StoreManagmentSystem.Service
{
    public interface ISalesService
    {
        Task<SaleResult> SellItem(SaleModel sale);
        Task<(bool Success, decimal Remaining, ShelfModel item)> TrySell(Guid productId, decimal quantityToSell);
    }
}
