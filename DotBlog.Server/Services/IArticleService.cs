using DotBlog.Server.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotBlog.Server.Services
{
    public interface IArticleService
    {
        // 获取相关

        /// <summary>
        /// [异步]获取首页文章列表(限制)
        /// </summary>
        /// <param name="limit">数量限制，为null则不限制</param>
        /// <returns>文章实例列表</returns>
        Task<ICollection<Article>> GetArticlesAsync(int? limit);

        /// <summary>
        /// [异步]获取首页文章列表(限制, 类别)
        /// </summary>
        /// <param name="limit">数量限制，为null则不限制</param>
        /// <param name="category">分类名</param>
        /// <returns>文章实例列表</returns>
        Task<ICollection<Article>> GetArticlesAsync(int? limit, string category);

        /// <summary>
        /// 通过文章ID获得文章实例
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>文章实例</returns>
        Article GetArticle(uint articleId);

        /// <summary>
        /// [异步]通过文章ID获得文章实例
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>文章实例</returns>
        Task<Article> GetArticleAsync(uint articleId);

        // 更新相关

        /// <summary>
        /// 更新文章的点赞数
        /// </summary>
        /// <param name="article">文章实体</param>
        /// <returns>更新结果</returns>
        void UpdateArticleLike(Article article);

        /// <summary>
        /// 更新文章已读数
        /// </summary>
        /// <param name="article">文章实体</param>
        /// <returns>更新结果</returns>
        void UpdateArticleRead(Article article);


        /// <summary>
        /// 更新文章内容
        /// </summary>
        /// <param name="articleOld">旧文章实体</param>
        /// <param name="article">新文章实体</param>
        /// <returns>更新结果</returns>
        Article UpdateArticle(Article articleOld, Article article);

        // 写入相关

        /// <summary>
        /// 写入新文章
        /// </summary>
        /// <param name="article">新文章Dto实体</param>
        /// <returns>保存结果</returns>
        Article PostArticle(Article article);

        // 删除相关

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="article">要删除的文章实体</param>
        /// <returns>删除结果</returns>
        void DeleteArticle(Article article);

        /// <summary>
        /// [异步]保存更改
        /// </summary>
        /// <returns>保存结果</returns>
        Task<bool> SaveChangesAsync();

        /// <summary>
        /// 保存更改
        /// </summary>
        /// <returns>保存结果</returns>
        bool SaveChanges();
    }
}
