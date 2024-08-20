
using MediatR;
using UrlShortenerService.Api.Endpoints;
using UrlShortenerService.Api.Endpoints.Url;

using UrlShortenerService.Application.Url.Commands;
using UrlShortenerService.Application.Url.Query;
using UrlShortenerService.Application.Url.Requests;

namespace Api.Endpoints.Url;
public class GetByCreatedBySummary : Summary<GetByCreatedByEndpoint>
{
    public GetByCreatedBySummary()
    {
        Summary = "Get the url by userid";
        Description =
            "this endpoint gives you the url created by a particualr user id";
        Response(404, "No short url found by that user id.");
        Response(500, "Internal server error.");
    }
}
public class GetByCreatedByEndpoint : BaseEndpoint<GetByCreatedByRequest>
{
    public GetByCreatedByEndpoint(ISender mediator, AutoMapper.IMapper mapper) : base(mediator, mapper)
    {
    }
    public override void Configure()
    {
        base.Configure();
        Get("by/{CreatedBy}");
        AllowAnonymous();
        Description(
            d => d.WithTags("Url")
        );
        Summary(new GetByCreatedBySummary());
    }

    public override async Task HandleAsync(GetByCreatedByRequest req, CancellationToken ct)
    {
        var result = await Mediator.Send(
            Mapper.Map<GetByCreatedByQuery>(req),
            ct
        );
        await SendOkAsync(Results.Ok(result));
    }
}
