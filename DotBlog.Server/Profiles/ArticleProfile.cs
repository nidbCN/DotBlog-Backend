using AutoMapper;
using DotBlog.Server.Dto;
using DotBlog.Server.Entities;
using DotBlog.Shared.Dto;
using System;

namespace DotBlog.Server.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            #region 从实体到Dto的映射
            CreateMap<Article, ArticleContentDto>()
                .ForMember(dest => dest.PostTime,
                    opt => opt.MapFrom(src => src.PostTime.ToString())
                );
            CreateMap<Article, ArticleListDto>()
                .ForMember(dest => dest.PostTime,
                    opt => opt.MapFrom(src => src.PostTime.ToString())
                );
            CreateMap<Article, ArticleUpdateDto>();
            #endregion

            #region 从Dto到实体的映射
            CreateMap<ArticleUpdateDto, Article>();
            CreateMap<ArticleAddDto, Article>()
                .ForMember(dest => dest.PostTime,
                    opt => opt.MapFrom(src => DateTime.Now.ToString())
                );
            #endregion
        }
    }
}
