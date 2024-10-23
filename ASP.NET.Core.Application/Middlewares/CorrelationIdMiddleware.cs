using Microsoft.Extensions.Primitives;

namespace ASP.NET.Core.Application.Middlewares;

public class CorrelationIdMiddleware(RequestDelegate next)
{
    const string CorrelationIdHeaderKey = "X-Correlation-ID";

    public async Task Invoke(HttpContext httpContext, ILogger<CorrelationIdMiddleware> logger)
    {
        string correlationId = Guid.NewGuid().ToString();

        if (httpContext.Request.Headers.TryGetValue(CorrelationIdHeaderKey, out StringValues _correlationId))
        {
            correlationId = _correlationId.ToString();
        }
        else
        {
            httpContext.Request.Headers.Add(CorrelationIdHeaderKey, correlationId);
        }


        NLog.MappedDiagnosticsContext.Set("CorrelationId", correlationId);

        logger.LogDebug($"Asp Dotnet Core, CorrelationId Log example >> {correlationId}");

        httpContext.Response.OnStarting(() =>
        {
            if (!httpContext.Response.Headers.TryGetValue(CorrelationIdHeaderKey, out _))
            {
                httpContext.Response.Headers.Add(CorrelationIdHeaderKey, correlationId);
            }

            return Task.CompletedTask;
        });

        httpContext.Items["CorrelationId"] = correlationId;
        await next(httpContext);
    }
}