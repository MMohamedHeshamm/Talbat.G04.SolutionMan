namespace Talbat.APIs.Errors
{
    public class ApiExceptionResponse : APiResponse
    {

        public string? Details { get; set; }

        public ApiExceptionResponse(int statusCode,string ? message,string ? details = null) : base(statusCode,message)
        {
            Details = details;
        }
    }
}
