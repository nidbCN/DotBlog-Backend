using DotBlog.Server.Data;
using DotBlog.Server.Entities;
using Masuit.Tools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotBlog.Server.Services
{
    public class ReplyService : IReplyService
    {
        private readonly DotBlogDbContext _context;

        public ReplyService(DotBlogDbContext context)
        {
            _context = context ??
                      throw new ArgumentNullException(nameof(context));
        }

        public async Task<Reply> GetReplyAsync(Article articleItem, uint replyId)
        {
            if (articleItem == null) throw new ArgumentNullException(nameof(articleItem));

            return await _context.Replies.FirstOrDefaultAsync(it => it.Article == articleItem);
        }

        //Task<Reply> GetReplyAsync(Article articleItem, uint replyId)
        //{

        //}

        // TODO(mail@gaein.cn): 科学的获取回复
        public async Task<ICollection<Reply>> GetRepliesAsync(Article article)
        {
            // 判空
            if (article == null) throw new ArgumentNullException(nameof(article));

            return await _context.Replies.Where(it => it.Article == article).ToListAsync();

            // return articleItem.Replies;
        }

        public void UpdateReplyLike(Reply reply)
        {
            // 判空
            if (reply == null) throw new ArgumentNullException(nameof(reply));

            // 自增
            reply.Like++;
        }

        public Reply PostReply(Article article, Reply reply)
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

        public void DeleteReply(Reply reply)
        {
            // 判空
            if (reply == null) throw new ArgumentNullException(nameof(reply));

            // 删除
            _context.Replies.Remove(reply);
        }


        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;

        public bool SaveChanges() =>
            _context.SaveChanges() > 0;
    }
}
