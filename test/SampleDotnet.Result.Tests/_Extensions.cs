namespace SampleDotnet.Result.Tests;

public static class _Extensions
{
    public static JsonSerializerSettings GetDefaultJsonSerializerForTesting()
    {
        return JsonConvert.DefaultSettings?.Invoke() ?? new JsonSerializerSettings();
    }

    public static void ShouldBeDefaultResponseModel(this BaseJsonResult response, ResponseModel responseModel)
    {
        response.SerializerSettings.ShouldBeEquivalentTo(GetDefaultJsonSerializerForTesting());
        response.Model.GetType().ShouldBeEquivalentTo(typeof(ResponseModel), "must be same type");

        response.ShouldSerializeAndDeserialize(responseModel);
    }

    public static void ShouldSerializeAndDeserialize<TTESTDATA>(this BaseJsonResult response, ResponseModel<TTESTDATA> previousResponseModel)
        where TTESTDATA : class
    {
        response.SerializerSettings.ShouldBeEquivalentTo(GetDefaultJsonSerializerForTesting());

        var jsonModel = JsonConvert.SerializeObject(response.Value, (JsonSerializerSettings?)response.SerializerSettings);
        jsonModel.ShouldNotBeNullOrWhiteSpace("must be has a value");

        ResponseModel<TTESTDATA>? objModel = JsonConvert.DeserializeObject<ResponseModel<TTESTDATA>>(jsonModel, (JsonSerializerSettings?)response.SerializerSettings);
        objModel.ShouldNotBeNull("shouldn't be null");
        objModel.ShouldBeEquivalentTo(previousResponseModel, "must be equivalent to previous generated value");
        objModel.GetType().ShouldBeEquivalentTo(typeof(ResponseModel<TTESTDATA>), "must be same type");
    }

    public static void ShouldSerializeAndDeserialize(this BaseJsonResult response, ResponseModel previousResponseModel)
    {
        response.SerializerSettings.ShouldBeEquivalentTo(GetDefaultJsonSerializerForTesting());

        var jsonModel = JsonConvert.SerializeObject(response.Value, (JsonSerializerSettings?)response.SerializerSettings);
        jsonModel.ShouldNotBeNullOrWhiteSpace("must be has a value");

        ResponseModel? objModel = JsonConvert.DeserializeObject<ResponseModel>(jsonModel, (JsonSerializerSettings?)response.SerializerSettings);
        objModel.ShouldNotBeNull("shouldn't be null");
        objModel.ShouldBeEquivalentTo(previousResponseModel, "must be equivalent to previous generated value");
        objModel.GetType().ShouldBeEquivalentTo(typeof(ResponseModel), "must be same type");
    }

    public static void ShouldValidatetCommons(this BaseJsonResult response, int statusCode)
    {
        typeof(BaseJsonResult).IsAssignableFrom(response.GetType()).ShouldBe(true, "invalid casting");

        response.StatusCode.ShouldNotBeNull("StatusCode must be defined");
        response.StatusCode.ShouldBe(statusCode, $"StatusCode must be equals to {statusCode}");

        response.ContentType.ShouldNotBeNullOrEmpty("ContentType must be defined");
        response.ContentType.ShouldBe(_Constants.ContentType_ApplicationJson, $"ContentType must be equals to {_Constants.ContentType_ApplicationJson}");

        response.Value.ShouldNotBeNull("Value must be defined");
    }
}