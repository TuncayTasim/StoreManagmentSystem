using Microsoft.EntityFrameworkCore;
using StoreManagmentSystem.Data;
using StoreManagmentSystem.Data.Entities;
using StoreManagmentSystem.Helpers;

public class ProductRepository:IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }

    public async Task<Product> GetProductById(Guid ProductId)
    {
        return await _context.Products.FindAsync(ProductId);
    }

    public async Task<Product> GetProductByBarcode(string barcode)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Barcode == barcode);
    }

    public async Task AddProduct(Product product)
    {
        var newProduct = new Product
        {
            Name = product.Name,
            TypeId = product.TypeId,
            Price = product.Price,
            Quantity = product.Quantity,
            DateAdded = product.DateAdded,
            ExpirationDate = product.ExpirationDate,
            Barcode = TokenGenerator.GenerateBulgarianEan13(),
            Note = product.Note,
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
}