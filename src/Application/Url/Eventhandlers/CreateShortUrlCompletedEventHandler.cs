using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using UrlShortenerService.Domain.Events;

namespace UrlShortenerService.Application.Url.Eventhandlers;
public class CreateShortUrlCompletedEventHandler : INotificationHandler<CreateShortUrlCompletedEvent>
{
    private readonly ILogger<CreateShortUrlCompletedEventHandler> _logger;
    public CreateShortUrlCompletedEventHandler(ILogger<CreateShortUrlCompletedEventHandler> logger)
    {
        _logger = logger;
    }
    public Task Handle(CreateShortUrlCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UrlService Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
