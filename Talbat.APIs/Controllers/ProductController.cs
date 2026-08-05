using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entites;
using Talabat.Core.Repoisitories.Contract;
using Talabat.Core.Specifications;

namespace Talbat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseAPIController
    {

        private readonly IGenericRepository<Product> _productRepo;

        public ProductController(IGenericRepository<Product> productRepo)
        {
               _productRepo = productRepo;
        }


        [HttpGet]
        public async Task<ActionResult<Product>> GetProducts()
        {

            var Spec = new ProductWithBrandAndTypeSpecifications();

            var Products = await _productRepo.GetAllAsync_Spec(Spec);

            return Ok(Products);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductsById( int id)
        {
            var Spec = new ProductWithBrandAndTypeSpecifications(id);

            var Products = await _productRepo.GetByIdAsync_Spec(Spec);

            return Ok(Products);

        }

    }
}
