using AutoMapper;
using DotBlog.Server.Dto;
using DotBlog.Server.Entities;
using DotBlog.Shared.Dto;
using Masuit.Tools;
using Masuit.Tools.Security;

namespace DotBlog.Server.Profiles
{
    public class ReplyProfile : Profile
    {
        public ReplyProfile()
        {
            // 从回复实体到输出Dto的映射
            CreateMap<Reply, ReplyContentDto>();

            // 从输入Dto到回复实体的映射
            CreateMap<ReplyAddDto, Reply>()
                .ForMember(dest => dest.AvatarUrl,
                    opt => opt
                        .MapFrom(src => string.IsNullOrEmpty(src.AvatarUrl) || !src.AvatarUrl.MatchUrl()
                            ? "https://gravatar.loli.net/avatar/" + src.Mail.MDString()
                            : src.AvatarUrl
                        )
                );
        }
    }
}
