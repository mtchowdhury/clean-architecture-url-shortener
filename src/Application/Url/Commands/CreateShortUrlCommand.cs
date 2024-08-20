using FluentValidation;
using HashidsNet;
using MediatR;
using UrlShortenerService.Application.Common.Interfaces;
using UrlShortenerService.Domain.Events;

namespace UrlShortenerService.Application.Url.Commands;

public record CreateShortUrlCommand : IRequest<string>
{
    public string Url { get; init; } = default!;
    public string User { get; set; }
}

public class CreateShortUrlCommandValidator : AbstractValidator<CreateShortUrlCommand>
{
    public CreateShortUrlCommandValidator()
    {
        _ = RuleFor(v => v.Url)
          .NotEmpty()
          .WithMessage("Url is required.");
       _ = RuleFor(v => v.Url).Must(IsValidUrl).WithMessage("invalid Url");
    }
    private bool IsValidUrl(string url)
    {
       return (Uri.TryCreate(url, UriKind.Absolute, out _)) ;
    }
}

public class CreateShortUrlCommandHandler : IRequestHandler<CreateShortUrlCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IHashids _hashids;
    private readonly IBaseUrl _baseurl;

    public CreateShortUrlCommandHandler(IApplicationDbContext context, IHashids hashids, IBaseUrl baseurl)
    {
        _context = context;
        _hashids = hashids;
        _baseurl = baseurl;
    }

    public async Task<string> Handle(CreateShortUrlCommand request, CancellationToken cancellationToken)
    {
        var hashid = string.Empty;
        var entity = new Domain.Entities.Url
        {
            OriginalUrl = request.Url,
            CreatedBy = request.User
        };
        entity.AddDomainEvent(new CreateShortUrlCompletedEvent(entity));
        _context.Urls.Add(entity);
       var result =  await _context.SaveChangesAsync(cancellationToken);
        hashid = _hashids.EncodeLong(entity.Id);

        
        return string.Concat( _baseurl.Url, "/u/", hashid);
    }
}
