using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;

namespace Api.Extensions;

/// <summary>
/// Validates against a single user from configuration. Stands in for a real user store,
/// which would replace this class behind <see cref="ICredentialValidator"/>.
/// </summary>
public class ConfiguredCredentialValidator : ICredentialValidator
{
    private readonly AuthSettings _settings;

    public ConfiguredCredentialValidator(IOptions<AuthSettings> settings)
    {
        _settings = settings.Value;
    }

    public bool IsValid(string username, string password)
    {
        if (string.IsNullOrEmpty(_settings.Username) || string.IsNullOrEmpty(_settings.Password))
            return false;

        return FixedTimeEquals(username, _settings.Username)
            & FixedTimeEquals(password, _settings.Password);
    }

    private static bool FixedTimeEquals(string? a, string b) =>
        CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(a ?? string.Empty),
            Encoding.UTF8.GetBytes(b));
}
