using System;
using AutoMapper;
using DotBlog.Server.Entities;
using DotBlog.Server.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace DotBlog.Server.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            // TODO(mail@gaein.cn): 添加各个字段的校验

            // 从文章实体到文章传输对象的映射
            CreateMap<Article, ArticleDto>()
                .ForMember(dest => dest.ResourceUri,
                    opt => opt
                        .MapFrom(src => $"Articles/{src.ArticleId}")
                );

            // 从文章实体到文章列表传输对象的映射
            CreateMap<Article, ArticleListDto>();
        }
    }
}
