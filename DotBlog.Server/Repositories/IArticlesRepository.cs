using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotBlog.Server.Entities;

namespace DotBlog.Server.Repositories
{
    public interface IArticlesRepository
    {
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
        public Task<IList<Article>> GetAsync(int offset, int count);

        /// <summary>
        /// 查找文章
        /// </summary>
        /// <param name="id">文章id</param>
        /// <returns>文章</returns>
        public Task<Article> FindAsync(int id);

        /// <summary>
        /// 查找所有文章
        /// </summary>
        /// <param name="match">表达式</param>
        /// <returns>文章列表</returns>
        public Task<IList<Article>> FindAllAsync(Predicate<Article> match);

        /// <summary>
        /// 查找所有文章
        /// </summary>
        /// <param name="match">表达式</param>
        /// <returns>文章列表</returns>
        public Task<IList<Article>> FindAllAsync(Predicate<Article> match, int? limit);
        
        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id">文章id</param>
        public void RemoveAt(int id);

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="match">表达式</param>
        public void RemoveAll(Predicate<Article> match);
    }
}
