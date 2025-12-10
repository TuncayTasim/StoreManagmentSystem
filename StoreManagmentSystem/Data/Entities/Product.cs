namespace StoreManagmentSystem.Data.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public DateTime DateAdded { get; set; }
        public DateTime ExpirationDate { get; set; }

        public string QRCode { get; set; }

        public string Note { get; set; }
    }
}
