using System.Security.Claims;
using Api.Endpoints.Authentication.Requests;
using Api.Extensions;
using FastEndpoints.Security;
using Microsoft.Extensions.Options;

namespace Api.Endpoints.Authentication;

public class AuthenticateEndpoint : Endpoint<LoginRequest>
{
    private readonly ICredentialValidator _credentialValidator;
    private readonly AuthSettings _settings;

    public AuthenticateEndpoint(ICredentialValidator credentialValidator, IOptions<AuthSettings> settings)
    {
        _credentialValidator = credentialValidator;
        _settings = settings.Value;
    }

    public override void Configure()
    {
        Post("/api/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        if (!_credentialValidator.IsValid(req.Username, req.Password))
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var token = JWTBearer.CreateToken(
            signingKey: _settings.Secret,
            expireAt: DateTime.UtcNow.AddMinutes(_settings.TokenLifetimeMinutes),
            claims: new[] { (ClaimTypes.NameIdentifier, req.Username) });

        await SendAsync(new { req.Username, Token = token }, cancellation: ct);
    }
}
