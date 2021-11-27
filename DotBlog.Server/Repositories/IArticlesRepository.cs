using DotBlog.Server.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotBlog.Server.Repositories
{
    public interface IArticlesRepository
    {
        #region 获取相关

        /// <summary>
        /// 获取所有文章
        /// </summary>
        /// <returns>文章列表</returns>
        public Task<IList<Article>> GetAllAsync();

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="offset">分页偏移</param>
        /// <param name="count">数据总数</param>
        /// <returns>文章列表</returns>
        public Task<IList<Article>> GetByPageAsync(int offset, int count);

        /// <summary>
        /// 获取文章
        /// </summary>
        /// <param name="id">文章id</param>
        /// <returns>文章</returns>
        public Task<Article> GetAsync(int id);

        /// <summary>
        /// 查找所有文章
        /// </summary>
        /// <param name="match">表达式</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        public Task<IList<Article>> FindAllAsync(Predicate<Article> match, int? count = null);

        #endregion

        #region 删除相关
        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id">文章id</param>
        public void Remove(Article article);

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="match">表达式</param>
        public void RemoveAll(Predicate<Article> match);
        #endregion

        #region 新建相关
        public void Add(Article article);
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
