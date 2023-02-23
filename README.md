[![nuget](https://img.shields.io/badge/nuget-SampleDotnet.Result-brightgreen.svg?maxAge=259200)](https://www.nuget.org/packages/SampleDotnet.Result)
[![build](https://github.com/msx752/SampleDotnet.Result/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/msx752/SampleDotnet.Result/actions/workflows/dotnet.yml)
[![nuget](https://img.shields.io/nuget/v/SampleDotnet.Result.svg)](https://www.nuget.org/packages/SampleDotnet.Result)

# SampleDotnet.Result
Single Response Model for the ActionResult

# How to Use
``` c#
using SampleDotnet.Result;
```
Success(Status:200) ResponseModel
``` c#
public IActionResult Get()
{
    List<CartDto> someResponseData = GetSomeData();
    
    return new OkResponse(someResponseData);
}

public IActionResult Get()
{
    CartDto singleData = GetSingleData();
    
    return new OkResponse(singleData);
}
```

NotFound(Status:404) ResponseModel
``` c#
public IActionResult Get()
{
    List<CartDto> someResponseData = GetSomeData();
    
    if (someResponseData.Count == 0)
        return new NotFoundResponse($"data not found.");

    return new OkResponse(someResponseData);
}
```

BadRequest(Status:400) ResponseModel
``` c#
public IActionResult Get(string search)
{
    if (string.NullOrEmpty(search))
        return new BadRequestResponse($"search value cannot be empty.");
        
    List<CartDto> someResponseData = GetSomeData();

    return new OkResponse(someResponseData);
}
```


# Serialized response model
``` json
{
    "results": [
        {
            // item 1
        },        
        {
            // item 2
        }
    ],
    "errors": [ 
        "errorMessage 1",
        "errorMessage 2"
    ],
    "stats": {
        "rid": "487f53e7c8316222f52c52ffe98ff5d7", // feature: unique request tracking id
        "offset": 1676841345, // feature: measured response time
        "elapsedmilliseconds": "342.8" // feature: measured response time
    }
}
```

# Additional Feature
- customizable SerializerSettings for each model
- Executor on before response model serialization
  - custom executor can be defined

# pre-defined executors
  - Unique request tracking id
``` c#
 // defined in ConfigureServices(IServiceCollection services)
services.AddSingleton<IBaseResultExecutor, ExecuteRequestTrackingId>();
```
  - Measured response time
``` c#
// defined in ConfigureServices(IServiceCollection services)
services.AddSingleton<IBaseResultExecutor, ExecuteMeasuredResponsTime>();

 // call in Configure(IApplicationBuilder app, IWebHostEnvironment env) before any middlewares
app.UseElapsedTimeMeasurement();
```

  - Custom executor
    - define class which derived from SampleProject.Result.Interfaces.IBaseResultExecutor
    ``` c#
    public class ExecuteAddVersionToResponseModel : IBaseResultExecutor
    {
        public Task OnBeforeActionResultExecutorAsync(
          HttpContext context
          , IServiceProvider serviceProvider
          , BaseJsonResult jsonResult)
        {
            return Task.Run(() =>
            {
                jsonResult.Model.Stats.version = "1.0.0";
            });
        }
    }
    ```
    - then add to ServiceCollection
    ``` c#
    // defined in ConfigureServices(IServiceCollection services)
    services.AddSingleton<IBaseResultExecutor, ExecuteAddVersionToResponseModel>();
    ```
