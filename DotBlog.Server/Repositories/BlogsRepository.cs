﻿using DotBlog.Server.Data;
using DotBlog.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotBlog.Server.Repositories
{
    public class BlogsRepository : IBlogsRepository
    {
        #region 私有字段
        /// <summary>
        /// 数据库上下文
        /// </summary>
        private readonly BlogDbContext _dbContext;

        /// <summary>
        /// 日志服务
        /// </summary>
        private readonly ILogger<BlogsRepository> _logger;
        #endregion

        #region 构造函数
        public BlogsRepository(BlogDbContext dbContext, ILogger<BlogsRepository> logger)
        {
            _dbContext = dbContext
                         ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger;
        }
        #endregion

        #region 公有方法

        #region 文章相关

        #region 获取相关

        public async Task<Article?> GetArticleAsync(int id)
            => await _dbContext.Articles.FirstOrDefaultAsync(x => x.ArticleId == id);

        public async Task<IList<Article>> GetAllArticlesAsync()
            => await _dbContext.Articles.ToListAsync();

        public async Task<IList<Article>> GetMatchedArticlesAsync(Predicate<Article> match, int page = 1, int? size = null)
        {
            if (match is null)
                throw new ArgumentNullException(nameof(match));

            var query = _dbContext.Articles.Where(x => match(x));

            if (size.HasValue)
                query = query.Skip(size.Value * (page - 1)).Take(size.Value);

            return await query.ToListAsync();
        }
        #endregion

        #region 删除相关
        public void RemoveArticle(Article article)
        {
            if (article is null)
                throw new ArgumentNullException(nameof(article));

            _dbContext.Articles.Remove(article);
        }

        public void RemoveMatchedArticles(Predicate<Article> match)
        {
            if (match is null)
                throw new ArgumentNullException(nameof(match));

            var searchResult = GetMatchedArticlesAsync(match).Result;
            foreach (var article in searchResult)
            {
                _dbContext.Articles.Remove(article);
            }

            _dbContext.SaveChanges();
        }
        #endregion

        #region 新建相关
        public void AddArticle(Article article)
        {
            if (article is null)
                throw new ArgumentNullException(nameof(article));

            _dbContext.Articles.Add(article);
        }

        #endregion

        #endregion

        #region 回复相关



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