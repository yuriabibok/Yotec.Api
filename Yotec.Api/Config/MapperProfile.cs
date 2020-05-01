using AutoMapper;
using Yotec.Api.Models;

namespace Yotec.Api.Config
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<dynamic, ArticleView>()
                .ForMember(d => d.Heading, opt => opt.MapFrom((s, v) => s.title))
                .ForMember(d => d.Link, opt => opt.MapFrom((s, v) => s.short_url))
                .ForMember(d => d.Updated, opt => opt.MapFrom((s, v) => s.updated_date));
        }
    }
}
