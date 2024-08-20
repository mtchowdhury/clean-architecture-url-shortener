using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortenerService.Domain.Common;
using UrlShortenerService.Domain.Entities;

namespace UrlShortenerService.Domain.Events;
public class CreateShortUrlCompletedEvent :BaseEvent
{
    public CreateShortUrlCompletedEvent(Url url)
    {
        Url = url;
    }
    public Url Url { get; }
}
