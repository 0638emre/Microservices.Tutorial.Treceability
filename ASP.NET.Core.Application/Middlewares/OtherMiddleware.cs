using NLog.Targets;

namespace ASP.NET.Core.Application.Middlewares;

public class OtherMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext httpContext, ILogger<OtherMiddleware> logger)
    {
        var correlationId = httpContext.Request.Headers["X-Correlation-ID"].FirstOrDefault();
        // ve ya
        correlationId = httpContext.Items["CorrelationId"].ToString();
        
        NLog.MappedDiagnosticsContext.Set("CorrelationId", correlationId);
        
        logger.LogInformation($"Other middleware logged >  CorrelationId: {correlationId}");
        
        await next(httpContext); 
    }
}