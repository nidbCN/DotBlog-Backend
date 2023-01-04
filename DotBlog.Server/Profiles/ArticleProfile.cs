using AutoMapper;
using DotBlog.Server.Dto;
using DotBlog.Server.Entities;
using DotBlog.Shared.Dto;
using System;

namespace DotBlog.Server.Profiles;

public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<Article, ArticleContentDto>()
            .ForMember(dest => dest.PostTime,
            opt => opt.MapFrom(src => src.PostTime.ToString())
            );
        CreateMap<Article, ArticleListDto>()
            .ForMember(dest => dest.PostTime,
            opt => opt.MapFrom(src => src.PostTime.ToString())
            );
        CreateMap<Article, ArticleUpdateDto>();
        CreateMap<ArticleUpdateDto, Article>();
        CreateMap<ArticleAddDto, Article>()
            .ForMember(dest => dest.PostTime,
            opt => opt.MapFrom(src => DateTime.Now.ToString())
            );
    }
}
