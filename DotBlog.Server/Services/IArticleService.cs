using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DotBlog.Server.Entities;

namespace DotBlog.Server.Services
{
    public interface IArticleService
    {
        // 获取相关
        /// <summary>
        /// 获取首页文章列表(限制)
        /// </summary>
        /// <returns>文章实例列表</returns>
        Task<List<Article>> GetArticlesAsync(int? limit);

        /// <summary>
        /// 获取首页文章列表(限制, 类别)
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="categoryId"></param>
        /// <returns>文章实例列表</returns>
        Task<List<Article>> GetArticlesAsync(int? limit, Guid categoryId);

        /// <summary>
        /// 通过文章ID获得文章实例
        /// </summary>
        /// <param name="articleId">文章实例</param>
        /// <returns>文章实例</returns>
        Article GetArticle(Guid articleId);

        /// <summary>
        /// 通过文章ID获得文章实例
        /// </summary>
        /// <param name="articleId">uint: 文章ID</param>
        /// <returns>文章实例</returns>
        Task<Article> GetArticleAsync(Guid articleId);

        // 更新相关

        /// <summary>
        /// 更新文章的点赞数
        /// </summary>
        /// <param name="articleItem">文章ID</param>
        /// <returns>更新结果</returns>
        bool PatchArticleLike(Article articleItem);

        /// <summary>
        /// 更新文章已读数
        /// </summary>
        /// <param name="articleItem">Guid: 文章ID</param>
        /// <returns>uint: 更新后的已读数</returns>
        bool PatchArticleRead(Article articleItem);


        /// <summary>
        /// 更新文章内容
        /// </summary>
        /// <param name="articleOld">Guid: 文章ID</param>
        /// <param name="article">ArticleContent: 文章内容类</param>
        /// <returns>bool: 保存结果</returns>
        Article PutArticle(Article articleOld, Article article);

        // 写入相关

        /// <summary>
        /// 写入新文章
        /// </summary>
        /// <param name="articleItem">ArticleItem: 新文章</param>
        /// <returns>bool: 保存结果</returns>
        Article PostArticle(Article articleItem);

        // 删除相关

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="articleItem"></param>
        /// <returns></returns>
        bool DeleteArticle(Article articleItem);
    }
}
