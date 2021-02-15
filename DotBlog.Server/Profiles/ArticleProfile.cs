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
            // 从文章实体到文章传输对象的映射
            CreateMap<Article, ArticleDto>();

            // 从文章实体到文章列表传输对象的映射
            CreateMap<Article, ArticleListDto>();

            // 添加对象到实体映射
            CreateMap<ArticleInputDto, Article>();
        }
    }
}
