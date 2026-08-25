using System.Net;
using System.Text.Json;

namespace Talbat.APIs.Middlewares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _Next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleWare(RequestDelegate Next, ILogger<ExceptionMiddleWare> logger, IHostEnvironment env)
        {
            this._Next = Next;
            this._logger = logger;
            this._env = env;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _Next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                #region MyRegion
                // for simplicity, we can use the following code to return the error Response based on the environment

                //if(_env.IsDevelopment())
                //{
                //    var Response = new Errors.ApiExceptionResponse(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString());
                //    await context.Response.WriteAsJsonAsync(Response);
                //}
                //else
                //{
                //    var Response = new Errors.ApiExceptionResponse(context.Response.StatusCode, "Internal Server Error");
                //    await context.Response.WriteAsJsonAsync(Response);
                //}

                #endregion


                // same but more elegant way using ternary operator
                var Response = _env.IsDevelopment()
                    ? new Errors.ApiExceptionResponse(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new Errors.ApiExceptionResponse(context.Response.StatusCode, "Internal Server Error");

                //for json file can use the following code
                //to serialize the Response object to json format and write it to the Response body
                var Options = new JsonSerializerOptions()
                {

                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var JsonResponse = JsonSerializer.Serialize(Response, Options);
                context.Response.WriteAsync(JsonResponse);

               


                
            }
        }

    }
}
