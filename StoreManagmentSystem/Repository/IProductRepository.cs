using StoreManagmentSystem.Data.Entities;

public interface IProductRepository
{
    Task AddProduct(Product product);
    Task DeleteProduct(Product product);
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product> GetProductById(Guid ProductId);
    Task UpdateProduct(Product product);
}