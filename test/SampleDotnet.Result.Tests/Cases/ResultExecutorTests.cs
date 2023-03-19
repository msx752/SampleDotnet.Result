using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SampleDotnet.Result.Executors;
using SampleDotnet.Result.Extensions;
using SampleDotnet.Result.Interfaces;
using SampleDotnet.Result.Tests.MockApiServer.Models;
using System.Net;

namespace SampleDotnet.Result.Tests.Cases;

public class ResultExecutorTests : MainControllerTests
{
    [Fact]
    public async Task ExecuteRequestTrackingId()
    {
        var client = CreateMockApiServer((services) =>
        {
            services.AddSingleton<IBaseResultExecutor, ExecuteRequestTrackingId>();
        },
        (app) =>
        {
            app.UseElapsedTimeMeasurement();
        });

        var response = await client.GetAsync("/api/Test/RequestOkResponseModel");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        ResponseModel<TestDatumDto> data = Response<ResponseModel<TestDatumDto>>(response);

        data.ShouldNotBeNull();
        data.Results.ShouldNotBeNull();
        data.Results.Count.ShouldBe(2);
        ((string)data.Stats.rid).ShouldNotBeNull();
    }

    [Fact]
    public async Task ExecuteMeasuredResponsTime()
    {
        var client = CreateMockApiServer((services) =>
        {
            services.AddSingleton<IBaseResultExecutor, ExecuteMeasuredResponsTime>();
        },
        (app) =>
        {
            app.UseElapsedTimeMeasurement();
        });

        var response = await client.GetAsync("/api/Test/RequestOkResponseModel");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        ResponseModel<TestDatumDto> data = Response<ResponseModel<TestDatumDto>>(response);

        data.ShouldNotBeNull();
        data.Results.ShouldNotBeNull();
        data.Results.Count.ShouldBe(2);
        ((long)data.Stats.offset).ShouldNotBe(0);
        ((string)data.Stats.elapsedmilliseconds).ShouldNotBeNull();
    }
}