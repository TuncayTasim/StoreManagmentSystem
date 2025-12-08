namespace StoreManagmentSystem.Data.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public int TypeId { get; set; }

        public decimal Price { get; set; }

        public double Quantity { get; set; }

        public DateTime DateAdded { get; set; }
        public DateTime ExpirationDate { get; set; }

        public string QRCode { get; set; }

        public string Note { get; set; }
    }
}
