namespace SampleDotnet.Result.Tests.Cases;

public class BadRequestResponseTests
{
    private const int statusCode_BADREQUEST = 400;

    [Fact]
    public void BadRequestResponse_DefaultWithNewtonsoftSerializer()
    {
        BadRequestResponse response = new BadRequestResponse() { SerializerSettings = new JsonSerializerSettings() };

        response.Model.Errors.ShouldBeNull();
        response.ShouldBeDefaultResponseModel(new ResponseModel());
    }

    [Fact]
    public void BadRequestResponse_Ctor1WithNewtonsoftSerializer()
    {
        string responseObject = "error message1";
        BadRequestResponse response = new BadRequestResponse(responseObject) { SerializerSettings = new JsonSerializerSettings() };

        response.ShouldValidatetCommons(statusCode_BADREQUEST);
        response.Model.ShouldNotBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel));

        ResponseModel responseModel = new ResponseModel(responseObject);
        response.ShouldSerializeAndDeserialize(responseModel);
    }

    [Fact]
    public void BadRequestResponse_Ctor2WithNewtonsoftSerializer()
    {
        IEnumerable<string> responseObjects = new List<string> { "error message1", "error message2" };
        BadRequestResponse response = new BadRequestResponse(responseObjects) { SerializerSettings = new JsonSerializerSettings() };

        response.ShouldValidatetCommons(statusCode_BADREQUEST);
        response.Model.ShouldNotBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel));

        ResponseModel responseModel = new ResponseModel(responseObjects);
        response.ShouldSerializeAndDeserialize(responseModel);
    }

    [Fact]
    public void StatusCodeResponse_Ctor1WithNewtonsoftSerializer()
    {
        string responseObject = "error message1";
        StatusCodeResponse response = new StatusCodeResponse(statusCode_BADREQUEST, responseObject) { SerializerSettings = new JsonSerializerSettings() };

        response.ShouldValidatetCommons(statusCode_BADREQUEST);
        response.Model.ShouldNotBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel));

        ResponseModel responseModel = new ResponseModel(responseObject);
        response.ShouldSerializeAndDeserialize(responseModel);
    }

    [Fact]
    public void StatusCodeResponse_Ctor2WithNewtonsoftSerializer()
    {
        IEnumerable<string> responseObjects = new List<string> { "error message1", "error message2" };
        StatusCodeResponse response = new StatusCodeResponse(statusCode_BADREQUEST, responseObjects) { SerializerSettings = new JsonSerializerSettings() };

        response.ShouldValidatetCommons(statusCode_BADREQUEST);
        response.Model.ShouldNotBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel));

        ResponseModel responseModel = new ResponseModel(responseObjects);
        response.ShouldSerializeAndDeserialize(responseModel);
    }
}