namespace SampleDotnet.Result.Tests.Cases;

public class NotFoundResponseTests
{
    private const int statusCode_NOTFOUND = 404;

    [Fact]
    public void NotFoundResponse_Default()
    {
        NotFoundResponse response = new NotFoundResponse();

        response.Model.Errors.ShouldBeNull();
        response.ShouldBeDefaultResponseModel(new ResponseModel());
    }

    [Fact]
    public void NotFoundResponse_Ctor1()
    {
        string responseObject = "NotFound error message1";
        NotFoundResponse response = new NotFoundResponse(responseObject);

        response.ShouldValidatetCommons(statusCode_NOTFOUND);
        response.Model.ShouldNotBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel));

        ResponseModel responseModel = new ResponseModel(responseObject);
        response.ShouldSerializeAndDeserialize(responseModel);
    }
}