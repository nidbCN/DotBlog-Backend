using DotBlog.Server.Data;
using DotBlog.Server.Entities;
using DotBlog.Server.Repositories;
using Masuit.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotBlog.Server.Services
{
    public class RepliesService : IRepliesService
    {
        #region 私有字段

        private readonly IBlogsRepository _blogsRepository;

        private readonly ILogger<RepliesService> _logger;

        #endregion

        #region 构造函数

        public RepliesService(IBlogsRepository blogsRepository, ILogger<RepliesService> logger)
        {
            _blogsRepository = blogsRepository;
            _logger = logger;
        }

        #endregion

        #region 公有方法

        public async Task<Reply?> GetAsync(uint articleId, uint replyId)
        {

            await _blogsRepository.GetReplyAsync((int)articleId, (int)replyId);
        }

        //Task<Reply> GetReplyAsync(Article articleItem, uint replyId)
        //{

        //}

        // TODO(mail@gaein.cn): 科学的获取回复
        public async Task<ICollection<Reply>> GetRepliesAsync(Article article)
        {
            // 判空
            if (article == null) throw new ArgumentNullException(nameof(article));

            return await _context.Replies
                .Where(it => it.Article == article)
                .ToListAsync();
        }

        public void Like(Reply reply)
        {
            // 判空
            if (reply == null) throw new ArgumentNullException(nameof(reply));

            // 自增
            reply.Like++;
        }

        public Reply? Add(Article article, Reply reply)
        {
            // 判空
            if (reply == null) throw new ArgumentNullException(nameof(reply));

            // 新建回复
            reply.ArticleId = article.ArticleId;

            if (!reply.Mail.MatchEmail().isMatch)
            {
                return null;
            }

            _context.Replies.Add(reply);
            return reply;
        }

        public void Delete(Reply reply)
        {
            // 判空
            if (reply is null)
                throw new ArgumentNullException(nameof(reply));

            // 删除
            _context.Replies.Remove(reply);
        }


        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;

        public bool SaveChanges() =>
            _context.SaveChanges() > 0;

        #endregion
    }
}
