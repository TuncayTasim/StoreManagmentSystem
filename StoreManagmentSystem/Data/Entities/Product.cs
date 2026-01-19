using StoreManagmentSystem.Helpers;

namespace StoreManagmentSystem.Data.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }

        public int BrandId { get; set; }

        public string Barcode { get; set; } = TokenGenerator.GenerateBulgarianEan13();

        public string Note { get; set; }

        public decimal QuantityInWarehouse { get; set; }

        public decimal QuantityInShelf { get; set; }

        
    }
}
