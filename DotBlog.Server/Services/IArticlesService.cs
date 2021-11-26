using DotBlog.Server.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotBlog.Server.Services
{
    public interface IArticlesService
    {
        #region 获取
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <returns>文章列表</returns>
        public Task<IList<Article>> GetAllAsync();

        /// <summary>
        /// 获取分页的文章列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="limit">数量</param>
        /// <returns>文章列表</returns>
        public Task<IList<Article>> GetAllAsync(int page, int limit);

        /// <summary>
        /// 按分类获取文章列表
        /// </summary>
        /// <param name="category">分类名称</param>
        /// <returns>文章列表</returns>
        public Task<IList<Article>> GetByCategoryAsync(string category);

        /// <summary>
        /// 按分类获取分页的文章列表
        /// </summary>
        /// <param name="category">分类名称</param>
        /// <param name="page">页码</param>
        /// <param name="limit">数量</param>
        /// <returns>文章列表</returns>
        public Task<IList<Article>> GetByCategoryAsync(string category, int page, int limit);

        /// <summary>
        /// 通过文章ID获得文章实例
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>文章实例</returns>
        public Task<Article> GetAsync(uint articleId);

        #endregion

        #region 更新相关

        /// <summary>
        /// 更新文章的点赞数
        /// </summary>
        /// <param name="articleId">文章id</param>
        /// <returns>更新结果</returns>
        public void Like(uint articleId);

        /// <summary>
        /// 更新文章已读数
        /// </summary>
        /// <param name="article">文章实体</param>
        /// <returns>更新结果</returns>
        public void Read(uint articleId);

        /// <summary>
        /// 更新文章内容
        /// </summary>
        /// <param name="articleOld">旧文章实体</param>
        /// <param name="article">新文章实体</param>
        /// <returns>更新结果</returns>
        public Article UpdateArticle(Article articleOld, Article article);

        #endregion

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
