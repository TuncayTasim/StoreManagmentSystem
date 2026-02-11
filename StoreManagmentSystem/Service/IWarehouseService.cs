using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.WarehouseModels;

namespace StoreManagmentSystem.Service
{
    public interface IWarehouseService
    {
        Task Restock(WarehouseModel stock);
        Task<IEnumerable<WarehouseModel>> GetAllRestocksByProductId(Guid ProductId);
        Task<IEnumerable<WarehouseRestock>> GetAllRestocks();
        Task<WarehouseRestock> GetRestockById(int StockId);
        Task<WarehouseModel> UpdateRestock(int StockId, WarehouseModelNoId stock);
        Task<IEnumerable<Warehouse>> GetAllStocksInWarehouse();
        Task<Warehouse> GetRestockRecordById(int warehouseId);
    }
}