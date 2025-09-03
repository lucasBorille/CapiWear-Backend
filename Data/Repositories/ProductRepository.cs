using CapiWear_API.Models;
using Microsoft.EntityFrameworkCore;
using CapiWear_API.Data;

namespace CapiWear_API.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApiDbContext _context;
        public ProductRepository(ApiDbContext context) => _context = context;

        public async Task<Product?> GetProductByIdAsync(int id)
            => await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
            => await _context.Products.AsNoTracking().ToListAsync();

        public async Task<Product> AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateProductAsync(Product product)
        {
            var existing = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
            if (existing is null) return null;

            _context.Entry(existing).CurrentValues.SetValues(product);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var existing = await _context.Products.FindAsync(id);
            if (existing is null) return false;

            _context.Products.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
