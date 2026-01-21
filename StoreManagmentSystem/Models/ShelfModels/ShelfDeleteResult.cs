namespace StoreManagmentSystem.Models.ShelfModels
{
    public class ShelfDeleteResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public bool RestockRecommended { get; set; }
        public decimal? QuantityInShelf { get; set; }
        public Data.Entities.ShelfRestock DeletedItem { get; set; }
    }


}
