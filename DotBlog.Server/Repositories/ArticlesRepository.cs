using DotBlog.Server.Data;
using DotBlog.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotBlog.Server.Repositories
{
    public class ArticlesRepository : IArticlesRepository
    {
        #region 私有字段
        /// <summary>
        /// 数据库上下文
        /// </summary>
        private readonly DotBlogDbContext _dbContext;

        /// <summary>
        /// 日志服务
        /// </summary>
        private readonly ILogger<ArticlesRepository> _logger;
        #endregion

        #region 构造函数
        public ArticlesRepository(DotBlogDbContext dbContext, ILogger<ArticlesRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        #endregion

        #region 公有方法

        #region 获取相关
        public async Task<IList<Article>> GetByPageAsync(int offset, int count)
            => await _dbContext.Articles.Skip(offset).Take(count).ToListAsync();

        public async Task<IList<Article>> GetAllAsync()
            => await _dbContext.Articles.ToListAsync();

        public async Task<Article> GetAsync(int id)
            => await _dbContext.Articles.FirstOrDefaultAsync(x => x.ArticleId == id);

        public async Task<IList<Article>> FindAllAsync(Predicate<Article> match, int? count = null)
        {
            if (match is null)
                throw new ArgumentNullException(nameof(match));

            var query = _dbContext.Articles.Where(x => match(x));

            if (count.HasValue)
                query = query.Take(count.Value);

            return await query.ToListAsync();
        }
        #endregion

        #region 删除相关
        public void Remove(Article article)
        {
            if (article is null)
                throw new ArgumentNullException(nameof(article));

            _dbContext.Articles.Remove(article);
        }

        public void RemoveAll(Predicate<Article> match)
        {
            if (match is null)
                throw new ArgumentNullException(nameof(match));

            var searchResult = FindAllAsync(match).Result;
            foreach (var article in searchResult)
            {
                _dbContext.Articles.Remove(article);
            }

            _dbContext.SaveChanges();
        }
        #endregion

        #region 新建相关
        public void Add(Article article)
        {
            if (article is null)
                throw new ArgumentNullException(nameof(article));

            _dbContext.Articles.Add(article);
        }

        #endregion

        #region 保存相关
        public void Save()
            => _dbContext.SaveChanges();

        public async Task SaveAsync()
            => await _dbContext.SaveChangesAsync();

        #endregion

        #endregion
    }
}
