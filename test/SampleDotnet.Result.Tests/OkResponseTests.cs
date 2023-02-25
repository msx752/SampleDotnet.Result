namespace SampleDotnet.Result.Tests;

public class OkResponseTests
{
    private const int statusCode_OK = 200;

    [Fact]
    public void OkResponse_Default()
    {
        OkResponse response = new OkResponse();

        response.Model.Errors.ShouldBeNull();
        response.ShouldValidatetCommons(statusCode_OK);
        response.ShouldBeDefaultResponseModel(new ResponseModel());
    }

    [Fact]
    public void OkResponse_Ctor1()
    {
        OkResponseTestData responseObject = new OkResponseTestData { strval1 = "valueOfName" };
        OkResponse response = new OkResponse(responseObject);

        response.ShouldValidatetCommons(statusCode_OK);
        response.Model.Errors.ShouldBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel<object>));

        ResponseModel<OkResponseTestData> responseModel = new ResponseModel<OkResponseTestData>(responseObject);
        response.ShouldSerializeAndDeserialize(responseModel);
    }

    [Fact]
    public void OkResponse_Ctor2()
    {
        IEnumerable<OkResponseTestData> responseObjects = new List<OkResponseTestData> { new OkResponseTestData() { strval1 = "valueOfName1", intval1 = 1 }, new OkResponseTestData { strval1 = "valueOfName2", intval1 = 2 } };
        OkResponse response = new OkResponse(responseObjects);

        response.ShouldValidatetCommons(statusCode_OK);
        response.Model.Errors.ShouldBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel<object>));

        ResponseModel<OkResponseTestData> responseModel = new ResponseModel<OkResponseTestData>(responseObjects);
        response.ShouldSerializeAndDeserialize(responseModel);
    }

    [Fact]
    public void StatusCodeResponse_Ctor1()
    {
        OkResponseTestData responseObject = new OkResponseTestData { strval1 = "valueOfName" };
        StatusCodeResponse response = new StatusCodeResponse(statusCode_OK, responseObject);

        response.ShouldValidatetCommons(statusCode_OK);
        response.Model.Errors.ShouldBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel<object>));

        ResponseModel<OkResponseTestData> responseModel = new ResponseModel<OkResponseTestData>(responseObject);
        response.ShouldSerializeAndDeserialize(responseModel);
    }

    [Fact]
    public void StatusCodeResponse_Ctor2()
    {
        IEnumerable<OkResponseTestData> responseObjects = new List<OkResponseTestData> { new OkResponseTestData() { strval1 = "valueOfName1", intval1 = 1 }, new OkResponseTestData { strval1 = "valueOfName2", intval1 = 2 } };
        StatusCodeResponse response = new StatusCodeResponse(statusCode_OK, responseObjects);

        response.ShouldValidatetCommons(statusCode_OK);
        response.Model.Errors.ShouldBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel<object>));

        ResponseModel<OkResponseTestData> responseModel = new ResponseModel<OkResponseTestData>(responseObjects);
        response.ShouldSerializeAndDeserialize(responseModel);
    }
}

internal class OkResponseTestData
{
    public int intval1 { get; set; }
    public string strval1 { get; set; }
}