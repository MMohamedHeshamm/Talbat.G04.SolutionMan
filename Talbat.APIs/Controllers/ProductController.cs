using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entites;
using Talabat.Core.Repoisitories.Contract;
using Talabat.Core.Specifications;
using Talbat.APIs.DTOs;
using Talbat.APIs.Errors;

namespace Talbat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseAPIController
    {

        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductCategory> _productCategoryRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;


        public ProductController(IGenericRepository<Product> productRepo,
            IMapper mapper , IGenericRepository<ProductCategory> ProductCategoryRepo, IGenericRepository<ProductBrand> ProductBrands)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _productCategoryRepo = ProductCategoryRepo;
            _productBrandRepo = ProductBrands;
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string sort)
        {

            var Spec = new ProductWithBrandAndTypeSpecifications(sort);

            var Products = await _productRepo.GetAll_Async_Spec(Spec);

            var mappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(Products);

            return Ok(mappedProducts);

        }




        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var Spec = new ProductWithBrandAndTypeSpecifications(id);

            var Products = await _productRepo.GetById_Async_Spec(Spec);

            if (Products is null)  return NotFound(new APiResponse(404)); 

            var mappedProducts = _mapper.Map<Product, ProductToReturnDto>(Products);

            return Ok(mappedProducts);

        }



        //Get All Product Categories
        [HttpGet("Categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetProductCategories()
        {

            var Categories = await _productCategoryRepo.GetAll_Async();
            return Ok(Categories);

        }

        //get all brands
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {

            var Brands = await _productBrandRepo.GetAll_Async();
            return Ok(Brands);
        }

    }
}
