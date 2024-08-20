using AutoMapper;
using UrlShortenerService.Application.Url.Commands;
using UrlShortenerService.Application.Url.Query;
using UrlShortenerService.Application.Url.Requests;

namespace UrlShortenerService.Application.Common.Mappings;
public class UrlMappingProfile : Profile
{
    public UrlMappingProfile()
    {
        CreateMap<CreateShortUrlRequest, CreateShortUrlCommand>().ReverseMap();
        CreateMap<GetByCreatedByRequest, GetByCreatedByQuery>().ForMember(dest => dest.Id, opt => opt.MapFrom(src =>src.CreatedBy)).ReverseMap();
    }
}
