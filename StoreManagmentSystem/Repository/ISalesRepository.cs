using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.SaleModels;
using StoreManagmentSystem.Models.ShelfModels;

namespace StoreManagmentSystem.Repository
{
    public interface ISalesRepository
    {
        Task<SaleResult> SellItem(Product product, decimal soldQuantity, ShelfModel item);
    }
}
