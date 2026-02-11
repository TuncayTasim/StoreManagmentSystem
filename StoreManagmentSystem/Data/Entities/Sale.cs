namespace StoreManagmentSystem.Data.Entities
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int ShelfId { get; set; }
        public decimal PriceSold { get; set; }
        public decimal QuantitySold { get; set; }
    }
}
