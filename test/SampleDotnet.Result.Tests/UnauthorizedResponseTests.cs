namespace SampleDotnet.Result.Tests;

public class UnauthorizedResponseTests
{
    private const int statusCode_UNAUTHORIZED = 401;

    [Fact]
    public void UnauthorizedResponse_Ctor1()
    {
        string responseObject = "Unauthorized message1";
        UnauthorizedResponse response = new UnauthorizedResponse(responseObject);

        response.ShouldValidatetCommons(statusCode_UNAUTHORIZED);
        response.Model.ShouldNotBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel));

        ResponseModel responseModel = new ResponseModel(responseObject);
        response.ShouldSerializeAndDeserialize(responseModel);
    }

    [Fact]
    public void UnauthorizedResponse_Ctor2()
    {
        IEnumerable<string> responseObjects = new List<string> { "Unauthorized message1", "Unauthorized message2" };
        UnauthorizedResponse response = new UnauthorizedResponse(responseObjects);

        response.ShouldValidatetCommons(statusCode_UNAUTHORIZED);
        response.Model.ShouldNotBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel));

        ResponseModel responseModel = new ResponseModel(responseObjects);
        response.ShouldSerializeAndDeserialize(responseModel);
    }

    [Fact]
    public void UnauthorizedResponse_Default()
    {
        UnauthorizedResponse response = new UnauthorizedResponse();

        response.Model.Errors.ShouldNotBeNull();
        response.ShouldBeDefaultResponseModel(new ResponseModel("Unauthorized"));
    }
}