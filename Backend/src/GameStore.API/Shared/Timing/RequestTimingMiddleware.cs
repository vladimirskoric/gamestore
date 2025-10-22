using System;
using System.Diagnostics;

namespace GameStore.API.Shared.Timing;

public class RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = new Stopwatch();

    try
    {
        stopwatch.Start();

        await next(context);
    }
    finally
    {
        stopwatch.Stop();

        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        logger.LogInformation("{Method} {Path} completed with status {Status} in {ElapseMilliseconds} ms.",
        context.Request.Method, context.Request.Path, context.Response.StatusCode, elapsedMilliseconds);
    }
    }
}
