using ASP.NET.Core.Application.Middlewares;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

#region Nlog Setup
builder.Logging.ClearProviders();
builder.Host.UseNLog();
#endregion

var app = builder.Build();

app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<OtherMiddleware>();

app.MapGet("/", (HttpContext httpContext, ILogger<Program> logger) =>
    {
        var correlationId = httpContext.Request.Headers["X-Correlation-ID"].FirstOrDefault();
        correlationId = httpContext.Items["CorrelationId"].ToString();
        
        NLog.MappedDiagnosticsContext.Set("CorrelationId", correlationId);

        logger.LogDebug("Minimal api is working now.");
    });

app.Run();