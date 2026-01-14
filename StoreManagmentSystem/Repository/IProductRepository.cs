using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.ProductModels;

public interface IProductRepository
{
    Task AddProduct(ProductModel product);
    Task DeleteProduct(Product product);
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product> GetProductByBarcode(string barcode);
    Task<Product> GetProductById(Guid ProductId);
    Task<ProductModel> GetProductModelById(Guid productId);
    Task UpdateProduct(Product product);
}