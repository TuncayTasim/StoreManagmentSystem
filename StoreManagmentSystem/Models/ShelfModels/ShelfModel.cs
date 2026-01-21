namespace StoreManagmentSystem.Models.ShelfModels
{
    public class ShelfModel
    {
        public Guid ProductId { get; set; }
        public decimal PriceSell { get; set; }
        public decimal QuantityRestocked { get; set; }
    }
}
