namespace StoreManagmentSystem.Models.WarehouseModels
{
    public class WarehouseDeleteResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public bool RestockRecommended { get; set; }
        public decimal? QuantityInWarehouse { get; set; }
        public Data.Entities.WarehouseRestock DeletedStock { get; set; }
    }

}
