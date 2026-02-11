namespace StoreManagmentSystem.Models.ShelfModels
{
    public class ShelfAddAndUpdateResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public decimal QuantityInWarehouse { get; set; }
        public decimal QuantityInShelf { get; set; }
        public Data.Entities.ShelfRestock ShelfItem { get; set; }
    }
}