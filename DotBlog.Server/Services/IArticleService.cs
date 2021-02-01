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
        /// 获取首页文章列表
        /// </summary>
        /// <returns>List[Article]: 文章实例列表</returns>
        Task<List<Article>> GetArticles(int? limit);

        /// <summary>
        /// 通过文章ID获得文章实例
        /// </summary>
        /// <param name="articleId">uint: 文章ID</param>
        /// <returns>Article: 文章内容实例</returns>
        Task<Article> GetArticle(Guid articleId);

        // 更新相关

        /// <summary>
        /// 更新文章的点赞数
        /// </summary>
        /// <param name="articleId">Guid: 文章ID</param>
        /// <returns>uint: 更新后的点赞数</returns>
        Task<bool> PatchArticleLike(Guid articleId);

        /// <summary>
        /// 更新文章已读数
        /// </summary>
        /// <param name="articleId">Guid: 文章ID</param>
        /// <returns>uint: 更新后的已读数</returns>
        Task<bool> PatchArticleRead(Guid articleId);


        /// <summary>
        /// 更新文章内容
        /// </summary>
        /// <param name="articleId">Guid: 文章ID</param>
        /// <param name="article">ArticleContent: 文章内容类</param>
        /// <returns>bool: 保存结果</returns>
        Task<Article> PutArticle(Guid articleId, Article article);

        // 写入相关

        /// <summary>
        /// 写入新文章
        /// </summary>
        /// <param name="article">ArticleItem: 新文章</param>
        /// <returns>bool: 保存结果</returns>
        Task<Article> PostArticle(Article article);

        // 删除相关
        
        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        Task<bool> DeleteArticle(Guid articleId);
    }
}
