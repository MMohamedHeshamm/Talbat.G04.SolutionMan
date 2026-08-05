using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entites;
using Talabat.Core.Repoisitories.Contract;
using Talabat.Core.Specifications;
using Talbat.APIs.DTOs;

namespace Talbat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseAPIController
    {

        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productRepo , IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<Product>> GetProducts()
        {

            var Spec = new ProductWithBrandAndTypeSpecifications();

            var Products = await _productRepo.GetAll_Async_Spec(Spec);

            var mappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(Products);

            return Ok(mappedProducts);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductsById(int id)
        {
            var Spec = new ProductWithBrandAndTypeSpecifications(id);

            var Products = await _productRepo.GetById_Async_Spec(Spec);

            var mappedProducts = _mapper.Map<Product, ProductToReturnDto>(Products);

            return Ok(mappedProducts);

        }

    }
}
