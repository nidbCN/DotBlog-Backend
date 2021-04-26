using DotBlog.Server.Data;
using DotBlog.Server.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DotBlog.Server.Services
{
    public class ArticleService : IArticleService
    {
        private readonly DotBlogDbContext _context;

        public ArticleService(DotBlogDbContext context)
        {
            _context = context ??
                       throw new ArgumentNullException(nameof(context));
        }


        #region 获取相关

        public async Task<ICollection<Article>> GetArticlesAsync(int? limit)
        {
            // 判空
            var limitNotNull = limit ?? -1;

            // 查询
            return await _context.Articles
                .OrderBy(it => it.PostTime.Ticks)
                .Take(limitNotNull)
                .ToListAsync();
        }

        public async Task<ICollection<Article>> GetArticlesAsync(int? limit, string category)
        {
            // 判空
            var limitNotNull = limit ?? -1;

            // 查询
            return await _context.Articles
                .Where(it => it.Category == category)
                .OrderBy(it => it.PostTime.Ticks)
                .Take(limitNotNull)
                .ToListAsync();
        }

        public async Task<Article> GetArticleAsync(uint articleId) =>
            await _context.Articles
                .FirstOrDefaultAsync(it => it.ArticleId == articleId);

        public Article GetArticle(uint articleId) =>
            _context.Articles
                .FirstOrDefault(it => it.ArticleId == articleId);

        #endregion


        #region 更新相关

        public void UpdateArticleLike(Article article)
        {
            // 判空
            article = article
                      ?? throw new ArgumentNullException(nameof(article));

            // 自增
            article.Like++;
        }

        public void UpdateArticleRead(Article article)
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

        public void DeleteArticle(Article article)
        {
            // 判空
            article = article
                      ?? throw new ArgumentNullException(nameof(article));

            // 删除文章
            _context.Articles.Remove(article);
        }

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;

        public bool SaveChanges() =>
            _context.SaveChanges() > 0;

        #endregion
    }
}
