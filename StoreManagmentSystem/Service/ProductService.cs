using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Models.ProductModels;

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

    public async Task<Product> GetProductByBarcode(string barcode)
    {
        return await _productRepository.GetProductByBarcode(barcode);
    }

    public async Task AddProduct(ProductModel product)
    {
        await _productRepository.AddProduct(product);
    }

    public async Task<ProductModel> UpdateProduct(Guid idToSearch, ProductModel newProduct)
    {
        var product = await _productRepository.GetProductById(idToSearch);

        if (product == null)
        {
            return null;
        }

        product.Name = newProduct.Name;
        product.TypeId = newProduct.TypeId;
        product.BrandId = newProduct.BrandId;
        product.Note = newProduct.Note;

        await _productRepository.UpdateProduct(product);
        var productModel = await _productRepository.GetProductModelById(idToSearch);
        return productModel;
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