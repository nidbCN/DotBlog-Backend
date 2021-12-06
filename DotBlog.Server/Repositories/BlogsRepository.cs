using DotBlog.Server.Data;
using DotBlog.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IList<Article>> GetMatchedArticlesAsync(Expression<Func<Article, bool>> match, int page = 1, int? size = null)
        {
            if (match is null)
                throw new ArgumentNullException(nameof(match));

            var query = _dbContext.Articles.Where(match).OrderBy(x => x.PostTime);

            if (!size.HasValue)
                return await query.ToListAsync();

            return await query.Skip(size.Value * (page - 1)).Take(size.Value).ToListAsync();
        }
        #endregion

        #region 删除相关
        public void RemoveArticle(Article article)
        {
            if (article is null)
                throw new ArgumentNullException(nameof(article));

            _dbContext.Articles.Remove(article);

            _dbContext.SaveChanges();
        }

        public void RemoveMatchedArticles(Predicate<Article> match)
        {
            if (match is null)
                throw new ArgumentNullException(nameof(match));

            var searchResult = GetMatchedArticlesAsync(x => match(x)).Result;
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

            _dbContext.SaveChanges();
        }

        #endregion

        #endregion

        #region 回复相关

        #region 获取相关

        public async Task<Reply?> GetReplyAsync(int articleId, int replyId)
            => await _dbContext.Replies
                .Where(x => x.ArticleId == articleId)
                .FirstOrDefaultAsync(x => x.ReplyId == replyId);

        public async Task<IList<Reply>?> GetAllRepliesAsync(int articleId)
        {
            if (!await _dbContext.Articles.AnyAsync(x => x.ArticleId == articleId))
                return null;

            return await _dbContext.Replies
                   .Where(x => x.ArticleId == articleId)
                   .ToListAsync();
        }

        #endregion

        #region 删除相关

        public void RemoveReply(Reply reply)
        {
            if (reply is null)
                throw new ArgumentNullException(nameof(reply));

            _dbContext.Replies.Remove(reply);

            _dbContext.SaveChanges();
        }

        #endregion

        #region 新建相关

        public void AddReply(Reply reply)
        {
            if (reply is null)
                throw new ArgumentNullException(nameof(reply));

            _dbContext.Replies.Add(reply);

            _dbContext.SaveChanges();
        }

        #endregion

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
