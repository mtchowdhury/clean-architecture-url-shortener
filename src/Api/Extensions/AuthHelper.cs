using System.Security.Claims;
using UrlShortenerService.Application.Common.Interfaces;

namespace Api.Extensions;

public  class AuthHelper : IUser
{
    private readonly IHttpContextAccessor _contextAccessor;
    public AuthHelper(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string? Id => _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

}
