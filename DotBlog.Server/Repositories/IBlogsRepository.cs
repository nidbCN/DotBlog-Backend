using DotBlog.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotBlog.Server.Repositories
{
    public interface IBlogsRepository
    {
        #region 文章相关

        #region 获取相关

        /// <summary>
        /// 获取文章
        /// </summary>
        /// <param name="id">文章id</param>
        /// <returns>文章</returns>
        public Task<Article?> GetArticleAsync(int id);

        /// <summary>
        /// 获取所有文章
        /// </summary>
        /// <returns>文章列表</returns>
        public Task<IList<Article>> GetAllArticlesAsync();

        /// <summary>
        /// 查找文章
        /// </summary>
        /// <param name="match">表达式</param>
        /// <param name="page">页码</param>
        /// <param name="size">页容量</param>
        /// <returns>文章列表</returns>
        public Task<IList<Article>> GetMatchedArticlesAsync(Expression<Func<Article, bool>> match, int page = 1, int? size = null);

        #endregion

        #region 删除相关
        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id">文章id</param>
        public void RemoveArticle(Article article);

        /// <summary>
        /// 删除符合条件的文章
        /// </summary>
        /// <param name="match">表达式</param>
        public void RemoveMatchedArticles(Predicate<Article> match);
        #endregion

        #region 新建相关
        public void AddArticle(Article article);
        #endregion

        #endregion

        #region 回复相关

        #region 获取相关

        /// <summary>
        /// 获取回复
        /// </summary>
        /// <param name="articleId">文章id</param>
        /// <param name="replyId">回复id</param>
        /// <returns>回复实体</returns>
        public Task<Reply?> GetReplyAsync(int articleId, int replyId);

        /// <summary>
        /// 获取回复列表
        /// </summary>
        /// <param name="articleId">文章id</param>
        /// <returns></returns>
        public Task<IList<Reply>?> GetAllRepliesAsync(int articleId);

        #endregion

        #region 删除相关

        /// <summary>
        /// 删除回复
        /// </summary>
        /// <param name="articleId">文章id</param>
        /// <param name="reply"> </param>
        public void RemoveReply(Reply reply);

        #endregion

        #region 新建相关

        public void AddReply(Reply reply);

        #endregion

        #endregion

        #region 保存相关
        /// <summary>
        /// 保存变更
        /// </summary>
        /// <returns></returns>
        public Task SaveAsync();

        /// <summary>
        /// 保存变更
        /// </summary>
        public void Save();
        #endregion
    }
}
