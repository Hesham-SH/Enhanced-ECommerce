using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly SiteContext _context;
        public ProductRepository(SiteContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id) => await _context.Products.FindAsync(id);


        public async Task<IReadOnlyList<Product>> GetProductsAsync() => await _context.Products.ToListAsync();
                        
    }
}