using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.ProductModels;

public interface IProductRepository
{
    Task AddProduct(ProductModel product);
    Task DeleteProduct(Product product);
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product> GetProductByBarcode(string barcode);
    Task<Product> GetProductById(Guid ProductId);
    Task UpdateProduct(Product product);
}