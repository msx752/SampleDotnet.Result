namespace SampleDotnet.Result.Abstractions;

public class BaseJsonResult : JsonResult
{
    public BaseJsonResult(int statusCode)
        : base(new ResponseModel())

    {
        StatusCode = statusCode;
        this.ContentType = _Constants.ContentType_ApplicationJson;
    }

    public BaseJsonResult(int statusCode, IEnumerable<string> errorMessages)
        : base(new ResponseModel(errorMessages))
    {
        StatusCode = statusCode;
        this.ContentType = _Constants.ContentType_ApplicationJson;
    }

    public BaseJsonResult(int statusCode, string errorMessages)
        : base(new ResponseModel(errorMessages))
    {
        StatusCode = statusCode;
        this.ContentType = _Constants.ContentType_ApplicationJson;
    }

    public BaseJsonResult(int statusCode, object body)
        : base(new ResponseModel<object>(body))
    {
        StatusCode = statusCode;
        this.ContentType = _Constants.ContentType_ApplicationJson;
    }

    public BaseJsonResult(int statusCode, IEnumerable<object> body)
        : base(new ResponseModel<object>(body), null)
    {
        StatusCode = statusCode;
        this.ContentType = _Constants.ContentType_ApplicationJson;
    }

    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    public IResponseModel Model
    {
        get
        {
            if (Value == null)
                throw new ArgumentNullException(nameof(Value));

            return (IResponseModel)Value;
        }
    }

    public override async Task ExecuteResultAsync(ActionContext context)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));

        if (StatusCode == null)
            throw new ArgumentNullException(nameof(StatusCode));

        var httpContext = context.HttpContext;
        httpContext.Response.StatusCode = StatusCode.Value;

        var serviceProvider = httpContext.RequestServices;
        var resultExecutors = serviceProvider.GetServices<IBaseResultExecutor>();

        foreach (var execute in resultExecutors)
            await execute.OnBeforeActionResultExecutorAsync(httpContext, serviceProvider, this);

        var executor = serviceProvider.GetService<IActionResultExecutor<JsonResult>>();
        if (executor == null)
            throw new ArgumentNullException(nameof(IActionResultExecutor<JsonResult>));

        if (this.Model.Stats is ExpandoObject eobj && !eobj.Any())
            this.Model.Stats = null;

        if (executor.GetType().Name == "NewtonsoftJsonResultExecutor")
            this.SerializerSettings = (JsonConvert.DefaultSettings?.Invoke() ?? new JsonSerializerSettings());
        else
            this.SerializerSettings = new System.Text.Json.JsonSerializerOptions();

        await executor.ExecuteAsync(context, this);
    }
}