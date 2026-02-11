using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.WarehouseModels;

namespace StoreManagmentSystem.Repository
{
    public interface IWarehouseRepository
    {
        Task Restock(WarehouseModel stock);
        Task<IEnumerable<WarehouseModel>> GetAllRestocksByProductId(Guid ProductId);
        Task<IEnumerable<WarehouseRestock>> GetAllRestocks();
        Task<WarehouseRestock> GetRestockById(int StockId);
        Task<WarehouseModel> GetRestockModelById(int stockId);
        Task<Warehouse> GetRestockRecordById(int warehouseId);
        Task<IEnumerable<Warehouse>> GetAllStocksInWarehouse();
        Task UpdateRestock(WarehouseRestock stock, Warehouse warehouseRecord);
        WarehouseModel MapToModel(WarehouseRestock stock, Warehouse restockRecord);
        Task<WarehouseRestock> GetStockEntityByModel(WarehouseModel stock);
        Task<Warehouse> GetWarehouseRecordByModel(WarehouseModel stock);
    }
}