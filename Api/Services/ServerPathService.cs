namespace CrmBackend.Api.Services;

public class ServerPathService(IHttpContextAccessor httpContextAccessor)
{
    public string GetServerPath()
    {
        return $"{GetProtocol()}://{GetHost()}";
    }

    public string GetProtocol()
    {
        var context = httpContextAccessor.HttpContext;
        var protocolString = (context!.Request.IsHttps ? "https" : "http") ?? "http";

        return protocolString;
    }

    public string GetHost()
    {
        var context = httpContextAccessor.HttpContext;
        var host = context?.Request.Host;

        return host!.Value.ToString();
    }
}
