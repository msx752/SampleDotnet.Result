namespace SampleDotnet.Result;

public class OkResponse
    : BaseJsonResult
{
    public OkResponse()
        : base(StatusCodes.Status200OK)
    {
    }

    public OkResponse(IEnumerable<object> body)
        : base(StatusCodes.Status200OK, body)
    {
    }

    public OkResponse(object body)
        : base(StatusCodes.Status200OK, body)
    {
    }
}