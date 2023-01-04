using AutoMapper;
using DotBlog.Server.Dto;
using DotBlog.Server.Entities;
using DotBlog.Shared.Dto;
using Masuit.Tools;
using Masuit.Tools.Security;

namespace DotBlog.Server.Profiles;

public class ReplyProfile : Profile
{
    public ReplyProfile()
    {
        const string formatterStr = "yyyy-M-d HH:mm:ss";
        // 从回复实体到输出Dto的映射
        CreateMap<Reply, ReplyContentDto>()
            .ForMember(dest => dest.ReplyTime,
            opt => opt.MapFrom(
                src => src.ReplyTime.ToString(formatterStr))
            );

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
