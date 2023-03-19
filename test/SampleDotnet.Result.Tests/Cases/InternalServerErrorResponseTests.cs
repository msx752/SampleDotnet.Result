namespace SampleDotnet.Result.Tests.Cases;

public class InternalServerErrorResponseTests
{
    private const int statusCode_INTERNALSERVERERROR = 500;

    [Fact]
    public void InternalServerErrorResponse_DefaultWithNewtonsoftSerializer()
    {
        InternalServerErrorResponse response = new InternalServerErrorResponse() { SerializerSettings = new JsonSerializerSettings() };

        response.Model.Errors.ShouldBeNull();
        response.ShouldBeDefaultResponseModel(new ResponseModel());
    }

    [Fact]
    public void InternalServerErrorResponse_Ctor1WithNewtonsoftSerializer()
    {
        string responseObject = "User Friendly Message1";
        InternalServerErrorResponse response = new InternalServerErrorResponse(responseObject) { SerializerSettings = new JsonSerializerSettings() };

        response.ShouldValidatetCommons(statusCode_INTERNALSERVERERROR);
        response.Model.ShouldNotBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel));

        ResponseModel responseModel = new ResponseModel(responseObject);
        response.ShouldSerializeAndDeserialize(responseModel);
    }

    [Fact]
    public void InternalServerErrorResponse_Ctor2WithNewtonsoftSerializer()
    {
        IEnumerable<string> responseObjects = new List<string> { "User Friendly Message1", "User Friendly Message2" };
        InternalServerErrorResponse response = new InternalServerErrorResponse(responseObjects) { SerializerSettings = new JsonSerializerSettings() };

        response.ShouldValidatetCommons(statusCode_INTERNALSERVERERROR);
        response.Model.ShouldNotBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel));

        ResponseModel responseModel = new ResponseModel(responseObjects);
        response.ShouldSerializeAndDeserialize(responseModel);
    }
}