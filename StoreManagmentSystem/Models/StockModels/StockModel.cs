namespace StoreManagmentSystem.Models.StockModels
{
    public class StockModel
    {
        public Guid ProductId { get; set; }
        public decimal PriceBought { get; set; }
        public decimal QuantityRestocked { get; set; }
        public int DaysToExpire { get; set; }
    }
}
