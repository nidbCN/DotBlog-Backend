using DotBlog.Server.Data;
using DotBlog.Server.Entities;
using DotBlog.Server.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DotBlog.Server.Services
{
    public class ArticlesService : IArticlesService
    {
        private readonly DotBlogDbContext _context;
        private readonly ILogger<ArticlesService> _logger;
        private readonly IArticlesRepository _repository;

        public ArticlesService(DotBlogDbContext context, ILogger<ArticlesService> logger, IArticlesRepository repository)
        {
            _context = context;
            _logger = logger;
            _repository = repository;
        }

        #region 获取相关

        public async Task<IList<Article>> GetAllAsync(int? limit)
        {
            if (limit.HasValue)
                return await _repository.GetAsync(0, limit.Value);

            return await _repository.GetAllAsync();
        }

        public async Task<IList<Article>> GetByCategory(int? limit, string category)
        {
            if (limit.HasValue)
                return await _repository.FindAllAsync(x => x.Category == category, limit.Value);

            return await _repository.FindAllAsync(x => x.Category == category);
        }

        public async Task<Article> GetAsync(uint articleId) =>
            await _context.Articles
                .FirstOrDefaultAsync(it => it.ArticleId == articleId);

        public Article GetArticle(uint articleId) =>
            _context.Articles
                .FirstOrDefault(it => it.ArticleId == articleId);

        #endregion


        #region 更新相关

        public void Like(Article article)
        {
            // 判空
            article = article
                      ?? throw new ArgumentNullException(nameof(article));

            // 自增
            article.Like++;
        }

        public void Read(Article article)
        {
            // 判空
            article = article
                      ?? throw new ArgumentNullException(nameof(article));

            // 自增
            article.Read++;
        }

        public Article UpdateArticle(Article articleOld, Article article)
        {

            // 判空
            articleOld = articleOld
                         ?? throw new ArgumentNullException(nameof(articleOld));
            _ = article
                      ?? throw new ArgumentNullException(nameof(article));

            // 更新文章内容
            // EF 自动追踪，不需要

            // 返回结果
            return articleOld;
        }

        #endregion


        #region 写入相关

        public Article PostArticle(Article article)
        {
            // 判空
            article = article
                      ?? throw new ArgumentNullException(nameof(article));

            // 赋值给article
            article.PostTime = DateTime.Now;
            // 添加文章
            _context.Articles.Add(article);
            // 返回结果
            return article;
        }

        #endregion


        #region 删除相关

        public void DeleteArticle(int articleId)
        {
            var count = _context.Articles.Count();

            for (var i = 0; i < count; i++)
            {
                if (articleId == i)
                {
                    _context.Articles.RemoveRange();
                }
            }
        }

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;

        public bool SaveChanges() =>
            _context.SaveChanges() > 0;

        public void DeleteArticle(Article article)
        {
            _context.Remove(article);
        }

        #endregion
    }
}
