using AutoMapper;
using DotBlog.Server.Entities;
using DotBlog.Server.Models;

namespace DotBlog.Server.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            // 从文章实体到输出Dto的映射
            CreateMap<Article, ArticleDto>();
            CreateMap<Article, ArticleListDto>();

            // 从输入Dto到文章实体的映射
            CreateMap<ArticleUpdateDto, Article>();
            CreateMap<ArticleAddDto, Article>();
        }
    }
}
