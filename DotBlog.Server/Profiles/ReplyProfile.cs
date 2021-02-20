using AutoMapper;
using DotBlog.Server.Entities;
using DotBlog.Shared.Dto;
using DotBlog.Server.Dto;

namespace DotBlog.Server.Profiles
{
    public class ReplyProfile : Profile
    {
        public ReplyProfile()
        {
            // 从回复实体到输出Dto的映射
            CreateMap<Reply, ReplyContentDto>();

            // 从输入Dto到回复实体的映射
            CreateMap<ReplyAddDto, Reply>();
        }
    }
}
