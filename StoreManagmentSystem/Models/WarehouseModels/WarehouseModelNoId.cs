namespace StoreManagmentSystem.Models.WarehouseModels
{
    public class WarehouseModelNoId
    {
        public decimal PriceBought { get; set; }
        public decimal RestockCurrentQuantity { get; set; }
        public int DaysToExpire { get; set; }
    }
}
