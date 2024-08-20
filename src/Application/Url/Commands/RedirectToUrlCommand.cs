using System.Net.Http.Headers;
using FluentValidation;
using HashidsNet;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShortenerService.Application.Common.Exceptions;
using UrlShortenerService.Application.Common.Interfaces;

namespace UrlShortenerService.Application.Url.Commands;

public record RedirectToUrlCommand : IRequest<string>
{
    public string Id { get; init; } = default!;
}

public class RedirectToUrlCommandValidator : AbstractValidator<RedirectToUrlCommand>
{
    public RedirectToUrlCommandValidator()
    {
        _ = RuleFor(v => v.Id)
          .NotEmpty()
          .WithMessage("Id is required.");
    }
}

public class RedirectToUrlCommandHandler : IRequestHandler<RedirectToUrlCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IHashids _hashids;

    public RedirectToUrlCommandHandler(IApplicationDbContext context, IHashids hashids)
    {
        _context = context;
        _hashids = hashids;
    }

    public async Task<string> Handle(RedirectToUrlCommand request, CancellationToken cancellationToken)
    {
        long longId = 0;
        try
        {
             longId = _hashids.DecodeSingleLong(request.Id);
        }
        catch (Exception ex)
        {
            throw new NotFoundException("No short url found.", ex.InnerException );
        }
        var entity = await _context.Urls.AsNoTracking().FirstOrDefaultAsync(x =>x.Id == longId);
        if (entity is null)
            throw new NotFoundException("No short url found.");
        return entity?.OriginalUrl == null? string.Empty:entity.OriginalUrl;

    }
}
