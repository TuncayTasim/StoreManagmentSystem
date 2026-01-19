namespace StoreManagmentSystem.Models.StockModels
{
    public class WarehouseModelNoId
    {
        public decimal PriceBought { get; set; }
        public int QuantityRestocked { get; set; }
        public int DaysToExpire { get; set; }
    }
}
