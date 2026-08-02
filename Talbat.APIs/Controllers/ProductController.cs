using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entites;
using Talabat.Core.Repoisitories.Contract;

namespace Talbat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseAPIController
    {

        private readonly IGenericRepostiry<Product> _productRepo;

        public ProductController(IGenericRepostiry<Product> productRepo)
        {
               _productRepo = productRepo;
        }
    }
}
