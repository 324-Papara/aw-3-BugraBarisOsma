using System.Text;

namespace Para.Api.Middleware;

public class RequestResponseLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggerMiddleware> _logger;

    public RequestResponseLoggerMiddleware(RequestDelegate next, ILogger<RequestResponseLoggerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        _logger.LogInformation("Request: {Method} {Url}", context.Request.Method, context.Request.Path);
    
        await _next(context);

        _logger.LogInformation("Response Status: {StatusCode}", context.Response.StatusCode);
    }

    
    private async Task<string> FormatRequest(HttpRequest request)
    {

        return $" Request: {request.Scheme} {request.Host}{request.Path} {request.QueryString} Body: {request.Body}";
    }

    private async Task<string> FormatResponse(HttpResponse response)
    {

        return $"Status: {response.StatusCode} Body: {response.Body}";
    }
}