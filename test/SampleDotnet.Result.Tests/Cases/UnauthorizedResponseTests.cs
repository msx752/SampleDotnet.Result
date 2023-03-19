namespace SampleDotnet.Result.Tests.Cases;

public class UnauthorizedResponseTests
{
    private const int statusCode_UNAUTHORIZED = 401;

    [Fact]
    public void UnauthorizedResponse_Ctor1WithNewtonsoftSerializer()
    {
        string responseObject = "Unauthorized message1";
        UnauthorizedResponse response = new UnauthorizedResponse(responseObject) { SerializerSettings = new JsonSerializerSettings() };

        response.ShouldValidatetCommons(statusCode_UNAUTHORIZED);
        response.Model.ShouldNotBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel));

        ResponseModel responseModel = new ResponseModel(responseObject);
        response.ShouldSerializeAndDeserialize(responseModel);
    }

    [Fact]
    public void UnauthorizedResponse_Ctor2WithNewtonsoftSerializer()
    {
        IEnumerable<string> responseObjects = new List<string> { "Unauthorized message1", "Unauthorized message2" };
        UnauthorizedResponse response = new UnauthorizedResponse(responseObjects) { SerializerSettings = new JsonSerializerSettings() };

        response.ShouldValidatetCommons(statusCode_UNAUTHORIZED);
        response.Model.ShouldNotBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel));

        ResponseModel responseModel = new ResponseModel(responseObjects);
        response.ShouldSerializeAndDeserialize(responseModel);
    }

    [Fact]
    public void UnauthorizedResponse_DefaultWithNewtonsoftSerializer()
    {
        UnauthorizedResponse response = new UnauthorizedResponse() { SerializerSettings = new JsonSerializerSettings() };

        response.Model.Errors.ShouldNotBeNull();
        response.ShouldBeDefaultResponseModel(new ResponseModel("Unauthorized"));
    }
}