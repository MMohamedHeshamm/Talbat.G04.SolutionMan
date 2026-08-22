using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entites;
using Talabat.Repoistory.Data;
using Talbat.APIs.Errors;

namespace Talbat.APIs.Controllers
{

    public class BuggyController : BaseAPIController
    {
        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("NotFound")]
        public ActionResult GetNotFoundRequest()
        {

            var product = _dbContext.Set<Product>().Find(100); // Assuming -1 is an invalid ID

            if (product is null)
            {
                // Return a 404 Not Found response with a null error message
                return NotFound( new APiResponse(404, null));
            }
            return Ok(product);
        }

        [HttpGet("ServerError")]
        public ActionResult GetServerError()
        {
            var product = _dbContext.Set<Product>().Find(100);
            
            // This will throw a NullReferenceException if product is null
            var ProductToReturn = product.ToString(); 

            return Ok(ProductToReturn);
 

        }


        [HttpGet("BadRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }


        //validation error type of bad request
        [HttpGet("BadRequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }

    }
}
