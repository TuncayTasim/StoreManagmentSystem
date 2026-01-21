namespace StoreManagmentSystem.Models.WarehouseModels
{
    public class WarehouseModel
    {
        public Guid ProductId { get; set; }
        public decimal PriceBought { get; set; }
        public decimal QuantityRestocked { get; set; }
        public int DaysToExpire { get; set; }
    }
}
