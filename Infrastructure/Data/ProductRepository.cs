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


        public async Task<Product> GetProductByIdAsync(int id) 
        {
            
            return await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .FirstOrDefaultAsync(p => p.Id == id);
        } 
        public async Task<IReadOnlyList<Product>> GetProductsAsync() 
        {
            return await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .ToListAsync();

        } 
        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync() => await _context.ProductTypes.ToListAsync();
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync() => await _context.ProductBrands.ToListAsync();
       

    }
}