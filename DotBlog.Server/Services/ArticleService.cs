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

        public async Task<List<Article>> GetArticlesAsync(int? limit)
        {
            // 判空limit
            var limitInt = (limit ??= -1);
            // 返回列表
            return await Context.Articles
                .OrderBy(it => it.PostTime.Ticks)
                .Take(limitInt)
                .ToListAsync();
        }

        public /*async*/ Task<List<Article>> GetArticlesAsync(int? limit, Guid categoryId)
        {
            // TODO(mail@gaein.cn): 支持分类
            throw new NotSupportedException();
        }

        /// <summary>
        /// 异步获取文章
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>文章Item</returns>
        public async Task<Article> GetArticleAsync(Guid articleId)
        {
            // 判空
            if (articleId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            return await Context.Articles
                .FirstOrDefaultAsync(it => it.ArticleId == articleId);
        }


        /// <summary>
        /// 获取文章
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>文章Item</returns>
        public Article GetArticle(Guid articleId)
        {
            // 判空
            if (articleId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            return Context.Articles
                .FirstOrDefault(it => it.ArticleId == articleId);
        }



        // 更新相关

        public bool PatchArticleLike(Article articleItem)
        {
            // 判空
            if (articleItem == null)
            {
                throw new ArgumentNullException(nameof(articleItem));
            }

            // 自增
            articleItem.Like++;
            return SaveChanges();
        }
        public bool PatchArticleRead(Article articleItem)
        {
            // 判空
            if (articleItem == null)
            {
                throw new ArgumentNullException(nameof(articleItem));
            }

            // 自增
            articleItem.Read++;
            return SaveChanges();
        }
        public Article PutArticle(Article articleOld, Article article)
        {
            // 判空
            if (articleOld == null)
            {
                throw new ArgumentNullException(nameof(articleOld));
            }
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
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
            
            // 返回结果
            return SaveChanges() ? articleOld : null;
        }


        // 写入相关

        public Article PostArticle(Article articleItem)
        {
            // 判空
            if (articleItem == null)
            {
                throw new ArgumentNullException(nameof(articleItem));
            }

            // 赋值给article
            var articleGuid = Guid.NewGuid();
            articleItem.ArticleId = articleGuid;
            articleItem.PostTime = DateTime.Now;
            articleItem.ResourceUri = $"/article/{articleGuid}";
            // 添加文章
            Context.Articles.Add(articleItem);
            // 返回结果
            return SaveChanges() ? articleItem : null;
        }


        // 删除相关

        public bool DeleteArticle(Article articleItem)
        {
            // 判空
            if (articleItem == null)
            {
                throw new ArgumentNullException(nameof(articleItem));
            }
            // 删除文章
            Context.Articles.Remove(articleItem);
            // 返回结果
            return SaveChanges();
        }

        ///// <summary>
        ///// 保存更改异步
        ///// </summary>
        ///// <returns>保存结果</returns>
        //private async Task<bool> SaveChangesAsync() =>
        //    await Context.SaveChangesAsync() > 0;

        /// <summary>
        /// 保存更改
        /// </summary>
        /// <returns>保存结果</returns>
        private bool SaveChanges() =>
            Context.SaveChanges() > 0;
    }
}
