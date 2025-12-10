using Microsoft.EntityFrameworkCore.Query.Internal;
using StoreManagmentSystem.Data.Entities;

public interface IProductService
{
    Task AddProduct(Product product);
    Task<Product> DeleteProduct(Guid ProductId);
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product> GetProductById(Guid ProductId);
    Task<Product> UpdateProduct(Guid idToSearch, Product newProduct);
}