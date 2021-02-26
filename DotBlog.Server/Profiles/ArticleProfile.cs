using AutoMapper;
using DotBlog.Server.Dto;
using DotBlog.Server.Entities;
using DotBlog.Shared.Dto;

namespace DotBlog.Server.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            const string formatterStr = "yyyy-M-d HH:mm:ss";
            // 从文章实体到输出Dto的映射
            CreateMap<Article, ArticleContentDto>()
                .ForMember(dest=>dest.PostTime,
                    opt=>
                        opt.MapFrom(src=>src.PostTime.ToString(formatterStr))
                );
            CreateMap<Article, ArticleListDto>()
                .ForMember(dest=>dest.PostTime,
                    opt =>
                        opt.MapFrom(src=>src.PostTime.ToString(formatterStr))
                );

            // 从输入Dto到文章实体的映射
            CreateMap<ArticleUpdateDto, Article>();
            CreateMap<ArticleAddDto, Article>();
        }
    }
}
