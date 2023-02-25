namespace SampleDotnet.Result
{
    public class StatusCodeResponse
        : BaseJsonResult
    {
        public StatusCodeResponse(int statusCode) : base(statusCode)
        {
        }

        public StatusCodeResponse(int statusCode, IEnumerable<string> errorMessages) : base(statusCode, errorMessages)
        {
        }

        public StatusCodeResponse(int statusCode, string errorMessages) : base(statusCode, errorMessages)
        {
        }

        public StatusCodeResponse(int statusCode, object body) : base(statusCode, body)
        {
        }

        public StatusCodeResponse(int statusCode, IEnumerable<object> body) : base(statusCode, body)
        {
        }
    }
}