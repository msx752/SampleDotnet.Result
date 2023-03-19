namespace SampleDotnet.Result.Tests.Cases;

public class InternalServerErrorResponseTests
{
    private const int statusCode_INTERNALSERVERERROR = 500;

    [Fact]
    public void InternalServerErrorResponse_Default()
    {
        InternalServerErrorResponse response = new InternalServerErrorResponse();

        response.Model.Errors.ShouldBeNull();
        response.ShouldBeDefaultResponseModel(new ResponseModel());
    }

    [Fact]
    public void InternalServerErrorResponse_Ctor1()
    {
        string responseObject = "User Friendly Message1";
        InternalServerErrorResponse response = new InternalServerErrorResponse(responseObject);

        response.ShouldValidatetCommons(statusCode_INTERNALSERVERERROR);
        response.Model.ShouldNotBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel));

        ResponseModel responseModel = new ResponseModel(responseObject);
        response.ShouldSerializeAndDeserialize(responseModel);
    }

    [Fact]
    public void InternalServerErrorResponse_Ctor2()
    {
        IEnumerable<string> responseObjects = new List<string> { "User Friendly Message1", "User Friendly Message2" };
        InternalServerErrorResponse response = new InternalServerErrorResponse(responseObjects);

        response.ShouldValidatetCommons(statusCode_INTERNALSERVERERROR);
        response.Model.ShouldNotBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel));

        ResponseModel responseModel = new ResponseModel(responseObjects);
        response.ShouldSerializeAndDeserialize(responseModel);
    }
}