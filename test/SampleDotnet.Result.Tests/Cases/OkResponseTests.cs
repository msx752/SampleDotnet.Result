namespace SampleDotnet.Result.Tests.Cases;

public class OkResponseTests
{
    private const int statusCode_OK = 200;

    [Fact]
    public void OkResponse_DefaultWithNewtonsoftSerializer()
    {
        OkResponse response = new OkResponse() { SerializerSettings = new JsonSerializerSettings() };

        response.Model.Errors.ShouldBeNull();
        response.ShouldValidatetCommons(statusCode_OK);
        response.ShouldBeDefaultResponseModel(new ResponseModel());
    }

    [Fact]
    public void OkResponse_Ctor1WithNewtonsoftSerializer()
    {
        OkResponseTestData responseObject = new OkResponseTestData { strval1 = "valueOfName" };
        OkResponse response = new OkResponse(responseObject) { SerializerSettings = new JsonSerializerSettings() };

        response.ShouldValidatetCommons(statusCode_OK);
        response.Model.Errors.ShouldBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel<object>));

        ResponseModel<OkResponseTestData> responseModel = new ResponseModel<OkResponseTestData>(responseObject);
        response.ShouldSerializeAndDeserialize(responseModel);
    }

    [Fact]
    public void OkResponse_Ctor2WithNewtonsoftSerializer()
    {
        IEnumerable<OkResponseTestData> responseObjects = new List<OkResponseTestData> { new OkResponseTestData() { strval1 = "valueOfName1", intval1 = 1 }, new OkResponseTestData { strval1 = "valueOfName2", intval1 = 2 } };
        OkResponse response = new OkResponse(responseObjects) { SerializerSettings = new JsonSerializerSettings() };

        response.ShouldValidatetCommons(statusCode_OK);
        response.Model.Errors.ShouldBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel<object>));

        ResponseModel<OkResponseTestData> responseModel = new ResponseModel<OkResponseTestData>(responseObjects);
        response.ShouldSerializeAndDeserialize(responseModel);
    }

    [Fact]
    public void StatusCodeResponse_Ctor1WithNewtonsoftSerializer()
    {
        OkResponseTestData responseObject = new OkResponseTestData { strval1 = "valueOfName" };
        StatusCodeResponse response = new StatusCodeResponse(statusCode_OK, responseObject) { SerializerSettings = new JsonSerializerSettings() };

        response.ShouldValidatetCommons(statusCode_OK);
        response.Model.Errors.ShouldBeNull();
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel<object>));

        ResponseModel<OkResponseTestData> responseModel = new ResponseModel<OkResponseTestData>(responseObject);
        response.ShouldSerializeAndDeserialize(responseModel);
    }

    [Fact]
    public void StatusCodeResponse_Ctor2WithNewtonsoftSerializer()
    {
        IEnumerable<OkResponseTestData> responseObjects = new List<OkResponseTestData> { new OkResponseTestData() { strval1 = "valueOfName1", intval1 = 1 }, new OkResponseTestData { strval1 = "valueOfName2", intval1 = 2 } };
        StatusCodeResponse response = new StatusCodeResponse(statusCode_OK, responseObjects) { SerializerSettings = new JsonSerializerSettings() };

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