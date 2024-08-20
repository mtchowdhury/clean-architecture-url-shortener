using System.Security.Claims;
using MediatR;
using UrlShortenerService.Application.Url.Commands;
using UrlShortenerService.Application.Url.Requests;
using IMapper = AutoMapper.IMapper;

namespace UrlShortenerService.Api.Endpoints.Url;

public class CreateShortUrlSummary : Summary<CreateShortUrlEndpoint>
{
    public CreateShortUrlSummary()
    {
        Summary = "Create short url from provided url";
        Description =
            "This endpoint will create a short url from provided original url.";
        Response(500, "Internal server error.");
    }
}

public class CreateShortUrlEndpoint : BaseEndpoint<CreateShortUrlRequest>
{
    public CreateShortUrlEndpoint(ISender mediator, IMapper mapper)
        : base(mediator, mapper) { }

    public override void Configure()
    {
        base.Configure();
        Post("u");
        AllowAnonymous();
        Description(
            d => d.WithTags("Url")
        );
        Summary(new CreateShortUrlSummary());
    }

    public override async Task HandleAsync(CreateShortUrlRequest req, CancellationToken ct)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await Mediator.Send(
            Mapper.Map<CreateShortUrlCommand>(req),
            ct
        );
        await SendOkAsync( Results.Ok(result));
    }
}
