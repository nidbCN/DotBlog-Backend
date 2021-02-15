
using AutoMapper;
using DotBlog.Server.Entities;
using DotBlog.Server.Models;

namespace DotBlog.Server.Profiles
{
    public class ArticleDtoProfile : Profile
    {
        public ArticleDtoProfile()
        {
            // 添加对象到实体映射
            CreateMap<ArticleInputDto, Article>();
        }
    }
}
