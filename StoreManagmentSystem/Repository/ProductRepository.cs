using Microsoft.EntityFrameworkCore;
using StoreManagmentSystem.Data;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Helpers;
using StoreManagmentSystem.Models.ProductModels;
using StoreManagmentSystem.Models.StockModels;

public class ProductRepository:IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _context.Products
       .AsNoTracking()
       .Select(u => new Product
       {
            ProductId = u.ProductId,
            Name = u.Name,
            TypeId = u.TypeId,
            BrandId = u.BrandId,
            Barcode = u.Barcode,
            Note = u.Note,

       })
       .ToListAsync();
    }

    public async Task<Product> GetProductById(Guid ProductId)
    {
        return await _context.Products.FindAsync(ProductId);
    }

    public async Task<Product> GetProductByBarcode(string barcode)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Barcode == barcode);
    }

    public async Task AddProduct(ProductModel product)
    {
        var newProduct = new Product
        {
            Name = product.Name,
            TypeId = product.TypeId,
            BrandId = product.BrandId,
            Note = product.Note
        };


        await _context.Products.AddAsync(newProduct);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();

    }
    public async Task DeleteProduct(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<ProductModel> GetProductModelById(Guid productId)
    {
        var product = await _context.Products.FindAsync(productId);
        return MapToModel(product);
    }
    public ProductModel MapToModel(Product product)
    {
        return new ProductModel
        {
            Name = product.Name,
            TypeId = product.TypeId,
            BrandId = product.BrandId,
            Note = product.Note

        };
    }
}