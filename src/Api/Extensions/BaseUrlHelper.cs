using System.Runtime.CompilerServices;
using UrlShortenerService.Application.Common.Interfaces;

namespace Api.Extensions;

public class BaseUrlHelper :IBaseUrl
{
    private readonly IHttpContextAccessor _contextAccessor;
    public BaseUrlHelper(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string? Url => GetBaseUrl();

    private string GetBaseUrl()
    {
        var baseURLShcema = _contextAccessor.HttpContext.Request.Scheme;
        var baseURL = _contextAccessor.HttpContext.Request.Host;
        return baseURLShcema+ "://" + baseURL;
    }
}
