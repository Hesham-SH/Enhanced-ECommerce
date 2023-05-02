using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly SiteContext _context;
        
       public ProductsController(SiteContext context)
       {
            _context = context;
        
       }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        [HttpGet("{id}")]
        public async Task<Product> GetSingleProducts(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product;
        }
    }
}