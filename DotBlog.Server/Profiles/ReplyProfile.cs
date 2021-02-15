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
            CreateMap<Reply, ReplyDto>();

            CreateMap<ReplyInputDto, Reply>();
        }
    }
}
