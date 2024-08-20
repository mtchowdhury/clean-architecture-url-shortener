namespace UrlShortenerService.Application.Url.Requests;

/// <summary>
/// Request model for the <see cref="UrlShortenerService.Api.Endpoints.Url.CreateShortUrlEndpoint"/>.
/// </summary>
public class CreateShortUrlRequest
{
    /// <summary>
    /// The long url to shorten.
    /// </summary>
    public string Url { get; set; } = default!;
}
