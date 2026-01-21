namespace StoreManagmentSystem.Data.Entities
{
    public class ShelfRestock
    {
        public int ShelfId { get; set; }
        public Guid ProductId { get; set; }
        public decimal PriceSell { get; set; }
        public decimal QuantityRestocked { get; set; }
        public DateTime RestockDate { get; set; }
    }
}
