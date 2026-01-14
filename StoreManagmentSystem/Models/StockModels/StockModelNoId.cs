namespace StoreManagmentSystem.Models.StockModels
{
    public class StockModelNoId
    {
        public decimal PriceBought { get; set; }
        public int QuantityRestocked { get; set; }
        public int DaysToExpire { get; set; }
    }
}
