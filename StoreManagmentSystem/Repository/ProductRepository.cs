using Microsoft.EntityFrameworkCore;
using StoreManagmentSystem.Data;
using StoreManagmentSystem.Data.Entities;

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

    public async Task AddProduct(Product product)
    {
        await _context.Products.AddAsync(product);
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