namespace StoreManagmentSystem.Models.SaleModels
{
    public class SaleResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public bool RestockRecommended { get; set; }
        public decimal? QuantityInShelf { get; set; }
    }
}
