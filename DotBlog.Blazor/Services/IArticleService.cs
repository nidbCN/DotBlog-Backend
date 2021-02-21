using DotBlog.Shared.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotBlog.Blazor.Services
{
    public interface IArticleService
    {
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <returns>文章列表</returns>
        Task<IEnumerable<ArticleListDto>> GetArticleListAsync();

        /// <summary>
        /// 获取文章内容
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <returns>文章内容</returns>
        Task<ArticleContentDto> GetArticleAsync(uint articleId);

        /// <summary>
        /// 更新文章点赞数
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <returns></returns>
        Task UpdateArticleLikeAsync(uint articleId);

        /// <summary>
        /// 更新文章阅读数
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <returns></returns>
        Task UpdateArticleReadAsync(uint articleId);
    }
}
