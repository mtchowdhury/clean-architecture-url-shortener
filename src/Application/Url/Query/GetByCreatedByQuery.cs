using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HashidsNet;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShortenerService.Application.Common.Exceptions;
using UrlShortenerService.Application.Common.Interfaces;

namespace UrlShortenerService.Application.Url.Query;
public class GetByCreatedByQuery : IRequest<string>
{
    public string Id { get; init; } = default!;
}
public class GetByCreatedByQueryValidator: AbstractValidator<GetByCreatedByQuery>
{
    public GetByCreatedByQueryValidator()
    {
        _ = RuleFor(v => v.Id)
             .NotEmpty()
            .WithMessage("Created By Id Is Required.");
    }

}

public class GetByCreatedByQueryHandler : IRequestHandler<GetByCreatedByQuery, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IHashids _hashids;
    public GetByCreatedByQueryHandler(IApplicationDbContext context, IHashids hashids)
    {
        _context = context;
        _hashids = hashids;
    }
    public async Task<string> Handle(GetByCreatedByQuery request, CancellationToken cancellationToken)
    {
        var entity = _context.Urls.FirstOrDefault(x=>x.CreatedBy == request.Id);
        if (entity == null)
        {
            throw new NotFoundException("No url with that createdby use found.");
        }
        return entity.OriginalUrl;
    }
}
