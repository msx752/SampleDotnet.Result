﻿namespace SampleDotnet.Result.Interfaces;

public interface IBaseResultExecutor
{
    Task OnBeforeActionResultExecutorAsync(HttpContext context, IServiceProvider serviceProvider, BaseJsonResult baseResult);
}