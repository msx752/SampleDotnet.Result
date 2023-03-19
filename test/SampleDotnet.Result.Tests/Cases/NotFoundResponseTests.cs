namespace SampleDotnet.Result.Tests.Cases;

public class NotFoundResponseTests
{
    private const int statusCode_NOTFOUND = 404;

    [Fact]
    public void NotFoundResponse_DefaultWithNewtonsoftSerializer()
    {
        NotFoundResponse response = new NotFoundResponse() { SerializerSettings = new JsonSerializerSettings() };

        response.Model.Errors.ShouldBeNull();
        response.ShouldBeDefaultResponseModel(new ResponseModel());
    }

    [Fact]
    public void NotFoundResponse_Ctor1WithNewtonsoftSerializer()
    {
        string responseObject = "NotFound error message1";
        NotFoundResponse response = new NotFoundResponse(responseObject) { SerializerSettings = new JsonSerializerSettings() };

        response.ShouldValidatetCommons(statusCode_NOTFOUND);
        response.Model.ShouldNotBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel));

        ResponseModel responseModel = new ResponseModel(responseObject);
        response.ShouldSerializeAndDeserialize(responseModel);
    }
}