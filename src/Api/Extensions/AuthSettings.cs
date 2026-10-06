namespace Api.Extensions;

/// <summary>
/// Bound from the "Auth" configuration section. Supply values through user-secrets or
/// environment variables; nothing sensitive is committed.
/// </summary>
public class AuthSettings
{
    public const string SectionName = "Auth";

    public string Secret { get; set; } = string.Empty;
    public int TokenLifetimeMinutes { get; set; } = 60;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
