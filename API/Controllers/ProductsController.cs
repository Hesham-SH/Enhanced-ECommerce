using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly IProductRepository _repo;
        
       public ProductsController(IProductRepository repo)
       {
            _repo = repo;
           
        
       }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts() => Ok(await _repo.GetProductsAsync()); 

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetSingleProduct(int id) => Ok(await _repo.GetProductByIdAsync(id));

        [HttpGet("types")]
        public async Task<ActionResult<ProductType>> GetProductTypes() => Ok(await _repo.GetProductTypesAsync());

        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetProductBrands() => Ok(await _repo.GetProductBrandsAsync());

    }
}