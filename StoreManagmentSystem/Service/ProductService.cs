using StoreManagmentSystem.Data.Entities;

public class ProductService:IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _productRepository.GetAllProducts();
    }

    public async Task<Product> GetProductById(Guid ProductId)
    {
        return await _productRepository.GetProductById(ProductId);
    }

    public async Task AddProduct(Product product)
    {
        await _productRepository.AddProduct(product);
    }

    public async Task<Product> UpdateProduct(Guid idToSearch, Product newProduct)
    {
        var product = await _productRepository.GetProductById(idToSearch);

        if (product == null)
        {
            return null;
        }

        product.Name = newProduct.Name;
        product.TypeId = newProduct.TypeId;
        product.Price = newProduct.Price;
        product.Quantity = newProduct.Quantity;
        product.DateAdded = newProduct.DateAdded;
        product.ExpirationDate = newProduct.ExpirationDate;
        product.QRCode = newProduct.QRCode;
        product.Note = newProduct.Note;

        await _productRepository.UpdateProduct(product);
        return product;
    }

    public async Task<Product> DeleteProduct(Guid ProductId)
    {
        var product = await _productRepository.GetProductById(ProductId);
        if (product == null)
        {
            return null;
        }
        await _productRepository.DeleteProduct(product);
        return product;
    }
}