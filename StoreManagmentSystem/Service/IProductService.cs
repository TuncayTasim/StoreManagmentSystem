using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.ProductModels;

public interface IProductService
{
    Task AddProduct(ProductModel product);
    Task<Product> DeleteProduct(Guid ProductId);
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product> GetProductByBarcode(string barcode);
    Task<Product> GetProductById(Guid ProductId);
    Task<ProductModel> UpdateProduct(Guid idToSearch, ProductModel newProduct);
}