
using AutoMapper;
using DotBlog.Server.Entities;
using DotBlog.Server.Models;

namespace DotBlog.Server.Profiles
{
    public class ReplyDtoProfile : Profile
    {
        public ReplyDtoProfile()
        {
            CreateMap<ReplyInputDto, Reply>();
        }
    }
}
