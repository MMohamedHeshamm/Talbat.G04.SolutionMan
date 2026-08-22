namespace Talbat.APIs.Errors
{
    public class ApiValidationErrorResponce : APiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        public ApiValidationErrorResponce(): base(400)
        {

            Errors = new List<string>();
        }

    }
}
