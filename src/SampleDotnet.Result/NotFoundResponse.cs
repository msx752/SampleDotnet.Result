﻿namespace SampleDotnet.Result;

public class NotFoundResponse
    : BaseJsonResult
{
    public NotFoundResponse()
        : base(StatusCodes.Status404NotFound)
    {
    }

    public NotFoundResponse(string message)
        : base(StatusCodes.Status404NotFound, message)
    {
    }
}