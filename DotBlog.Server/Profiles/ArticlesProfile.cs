﻿using AutoMapper;
using DotBlog.Server.Entities;
using DotBlog.Server.Models;

namespace DotBlog.Server.Profiles
{
    public class ArticlesProfile : Profile
    {
        public ArticlesProfile()
        {
            CreateMap<Article, ArticlesDto>().ForMember(dest => dest.ResourceUri, 
                opt => opt
                    .MapFrom(src => $"Articles/{src.ArticleId}")
            );
        }
    }
}