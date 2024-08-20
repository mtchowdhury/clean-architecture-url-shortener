namespace UrlShortenerService.Application.Url.Requests;
/// <summary>
/// Request model for the <see cref="UrlShortenerService.Api.Endpoints.Url.CreateShortUrlEndpoint"/>.
/// </summary>
public class GetByCreatedByRequest
{
    /// <summary>
    /// The created by Id to fetch by.
    /// </summary>
    public string CreatedBy { get; set; } = default!;
}
