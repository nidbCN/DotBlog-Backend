using AutoMapper;
using DotBlog.Server.Entities;
using DotBlog.Server.Models;

namespace DotBlog.Server.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleDto>()
                .ForMember(dest => dest.ResourceUri,
                    opt => opt
                        .MapFrom(src => $"Articles/{src.ArticleId}")
                );
        }
    }
}
