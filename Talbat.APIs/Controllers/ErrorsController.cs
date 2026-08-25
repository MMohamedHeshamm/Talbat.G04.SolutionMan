using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Talbat.APIs.Errors;

namespace Talbat.APIs.Controllers
{
    [Route("errror/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorsController : ControllerBase
    {

        public ActionResult Error(int code)
        {
            return NotFound(new APiResponse (code));
        }

    }
}
