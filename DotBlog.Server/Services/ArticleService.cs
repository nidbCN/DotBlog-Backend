using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using DotBlog.Server.Data;
using DotBlog.Server.Entities;


namespace DotBlog.Server.Services
{
    public class ArticleService : IArticleService
    {
        private DotBlogDbContext Context { get; }

        public ArticleService(DotBlogDbContext context)
        {
            Context = context;
        }


        // 获取相关

        public async Task<ICollection<Article>> GetArticlesAsync(int? limit)
        {
            // 判空
            var limitNotNull = limit ?? -1;

            // 查询
            return await Context.Articles
                .OrderBy(it => it.PostTime.Ticks)
                .Take(limitNotNull)
                .ToListAsync();
        }

        public async Task<ICollection<Article>> GetArticlesAsync(int? limit, string category)
        {
            // 判空
            var limitNotNull = limit ?? -1;

            // 查询
            return await Context.Articles
                .Where(it => it.Category == category)
                .OrderBy(it => it.PostTime.Ticks)
                .Take(limitNotNull)
                .ToListAsync();
        }

        public async Task<Article> GetArticleAsync(uint articleId) =>
            await Context.Articles
                .FirstOrDefaultAsync(it => it.ArticleId == articleId);

        public Article GetArticle(uint articleId) =>
            Context.Articles
                .FirstOrDefault(it => it.ArticleId == articleId);


        // 更新相关

        public void PatchArticleLike(Article article)
        {
            // 判空
            article = article
                      ?? throw new ArgumentNullException(nameof(article));

            // 自增
            article.Like++;
        }

        public void PatchArticleRead(Article article)
        {
            // 判空
            article = article
                      ?? throw new ArgumentNullException(nameof(article));

            // 自增
            article.Read++;
        }

        public Article PutArticle(Article articleOld, Article article)
        {

            // 判空
            articleOld = articleOld 
                         ?? throw new ArgumentNullException(nameof(articleOld));
            article = article 
                      ?? throw new ArgumentNullException(nameof(article));

            // 更新文章内容
            // TODO(mail@gaein.cn): 用更好的方法来实现这一段代码
            articleOld.Alias = article.Alias;
            articleOld.Author = article.Author;
            articleOld.Category = article.Category;
            articleOld.Content = article.Content;
            articleOld.Like = article.Like;
            articleOld.Description = article.Description;
            articleOld.IsShown = article.IsShown;

            // 返回结果
            return articleOld;
        }


        // 写入相关

        public Article PostArticle(Article article)
        {
            // 判空
            article = article
                      ?? throw new ArgumentNullException(nameof(article));

            // 赋值给article
            article.PostTime = DateTime.Now;
            // 添加文章
            Context.Articles.Add(article);
            // 返回结果
            return article;
        }


        // 删除相关

        public void DeleteArticle(Article article)
        {
            // 判空
            article = article
                      ?? throw new ArgumentNullException(nameof(article));

                // 删除文章
            Context.Articles.Remove(article);
        }

        public async Task<bool> SaveChangesAsync() =>
            await Context.SaveChangesAsync() > 0;

        public bool SaveChanges() =>
            Context.SaveChanges() > 0;
    }
}
