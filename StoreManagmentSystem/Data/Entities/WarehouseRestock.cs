using StoreManagmentSystem.Helpers;

namespace StoreManagmentSystem.Data.Entities
{
    public class WarehouseRestock
    {
        public int WarehouseId { get; set; }
        public Guid ProductId { get; set; }
        public decimal PriceBought { get; set; }
        public decimal QuantityRestocked { get; set; }
        public DateTime RestockDate { get; set; } = DateTime.UtcNow;
        public int DaysToExpire { get; set; }
    }

        
}
