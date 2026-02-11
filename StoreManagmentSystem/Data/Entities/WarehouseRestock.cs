namespace StoreManagmentSystem.Data.Entities
{
    public class WarehouseRestock
    {
        public int StockId { get; set; }
        public decimal PriceBought { get; set; }
        public int DaysToExpire { get; set; }
        public int WarehouseId { get; set; }
    }

        
}
