namespace StoreManagmentSystem.Data.Entities
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }
        public int ActionId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime ActionDate { get; set; } = DateTime.UtcNow;
        public decimal CurrentQuantity { get; set; }
    }
}
