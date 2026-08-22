namespace Talbat.APIs.Errors
{
    public class APiResponse
    {

        public int StatusCode { get; set; }
        public string? Message { get; set; }


        public APiResponse(int status, string? message = null)
        {
            StatusCode = status;
            Message = message ?? GetDefaultMessage(status);

        }

        private string? GetDefaultMessage(int? statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",  // for 
                401 => "Unauthorized",    // for Unauthorized 
                404 => "Resource Not Found", // for Not found
                500 => "Internal Server Error", // server error
                _ => null
            };
        }
    }
}
