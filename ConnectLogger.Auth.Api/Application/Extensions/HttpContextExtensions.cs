namespace ConnectLogger.Auth.Api.Application.Extensions;

public static class HttpContextExtensions
{
    public static string? GetClientIpAddressString(this HttpContext httpContext)
    {
        var clientIpAddress = httpContext.GetClientIpAddressFromHeader();

        if (string.IsNullOrWhiteSpace(clientIpAddress)
            && httpContext.Connection.RemoteIpAddress != null)
        {
            clientIpAddress = httpContext.Connection.RemoteIpAddress.ToString();
        }

        return clientIpAddress;
    }

    public static string? GetClientIpAddressFromHeader(this HttpContext httpContext)
    {
        if (!httpContext.Request.Headers.TryGetValue("X-Forwarded-For", out var xForwardedForValue))
        {
            return null;
        }

        var xForwardedFor = xForwardedForValue.ToString();
        if (string.IsNullOrWhiteSpace(xForwardedFor))
        {
            return null;
        }

        var clientIpAddress = xForwardedFor.Split(',').FirstOrDefault()?.Trim();
        return clientIpAddress;
    }
}
