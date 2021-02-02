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

        public async Task<List<Article>> GetArticles(int? limit)
        {
            var limitInt = limit ??= -1;

            return await Context.Articles
                .OrderBy(it => it.PostTime.Ticks)
                .Take(limitInt)
                .ToListAsync();
        }
        public async Task<Article> GetArticle(Guid articleId) =>
            await Context.Articles
                .FirstOrDefaultAsync(it => it.ArticleId == articleId);


        // 更新相关

        public async Task<bool> PatchArticleLike(Guid articleId)
        {
            var article = await GetArticle(articleId);
            if (article == null)
            {
                return false;
            }

            article.Like++;
            return await SaveChanges();
        }
        public async Task<bool> PatchArticleRead(Guid articleId)
        {
            var article = await GetArticle(articleId);
            if (article == null)
            {
                return false;
            }

            article.Read++;

            return await SaveChanges();
        }
        public async Task<Article> PutArticle(Guid articleId, Article article)
        {
            var articleOld = await GetArticle(articleId);
            if (articleOld == null)
            {
                return null;
            }

            // 更新文章内容
            // TODO(mail@gaein.cn): 用更好的方法来实现这一段代码
            articleOld.Alias = article.Alias;
            articleOld.Author = article.Author;
            articleOld.Category = article.Category;
            articleOld.Content = article.Content;
            articleOld.Like = article.Like;
            articleOld.Description = article.Description;
            articleOld.IsShown = article.IsShown;

            return await SaveChanges() ? articleOld : null;
        }


        // 写入相关

        public async Task<Article> PostArticle(Article article)
        {
            var articleId = Guid.NewGuid();
            article.ArticleId = articleId;
            article.PostTime = DateTime.Now;
            article.ResourceUri = $"/article/{articleId}";
            await Context.Articles
                .AddAsync(article);
            return await SaveChanges() ? article: null;
        }


        // 删除相关

        public async Task<bool> DeleteArticle(Guid articleId)
        {
            Context.Articles.Remove(
                await GetArticle(articleId)
            );

            return await SaveChanges();
        }

        /// <summary>
        /// 保存更改
        /// </summary>
        /// <returns>保存结果</returns>
        private async Task<bool> SaveChanges() =>
            await Context.SaveChangesAsync() > 0;
    }
}
