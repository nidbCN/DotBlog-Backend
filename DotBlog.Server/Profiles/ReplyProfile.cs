using System;
using AutoMapper;
using DotBlog.Server.Entities;
using DotBlog.Server.Models;

namespace DotBlog.Server.Profiles
{
    public class ReplyProfile : Profile
    {
        public ReplyProfile()
        {
            CreateMap<Reply, ReplyDto>()
                .ForMember(dest => dest.ResourceUri,
                    opt => opt
                        .MapFrom(src => $"Articles/{src.ArticleId}/Replies/{src.ReplyId}")
                );
        }
    }
}
