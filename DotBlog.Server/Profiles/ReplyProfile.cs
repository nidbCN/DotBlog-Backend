using AutoMapper;
using DotBlog.Server.Entities;
using DotBlog.Server.Models;

namespace DotBlog.Server.Profiles
{
    public class ReplyProfile : Profile
    {
        public ReplyProfile()
        {
            // 从回复实体到输出Dto的映射
            CreateMap<Reply, ReplyDto>();

            // 从输入Dto到回复实体的映射
            CreateMap<ReplyAddDto, Reply>();
        }
    }
}
