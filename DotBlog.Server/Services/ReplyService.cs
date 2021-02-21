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
        private DotBlogDbContext Context { get; }

        public ReplyService(DotBlogDbContext context)
        {
            Context = context;
        }

        public async Task<Reply> GetReplyAsync(Article articleItem, uint replyId)
        {
            if (articleItem == null)
            {
                throw new ArgumentNullException(nameof(articleItem));
            }

            return await Context.Replies.FirstOrDefaultAsync(it => it.Article == articleItem);
        }

        //Task<Reply> GetReplyAsync(Article articleItem, uint replyId)
        //{

        //}

        // TODO(mail@gaein.cn): 科学的获取回复
        public async Task<ICollection<Reply>> GetRepliesAsync(Article article)
        {
            // 判空
            article = article
                      ?? throw new ArgumentNullException(nameof(article));

            return await Context.Replies.Where(it => it.Article == article).ToListAsync();

            // return articleItem.Replies;
        }

        public void PatchReplyLike(Reply reply)
        {
            // 判空
            reply = reply
                    ?? throw new ArgumentNullException(nameof(reply));

            // 自增
            reply.Like++;
        }

        public Reply PostReply(Article article, Reply reply)
        {
            // 判空
            reply = reply
                    ?? throw new ArgumentNullException(nameof(reply));


            // 新建回复
            reply.ArticleId = article.ArticleId;

            if (!reply.Mail.MatchEmail().isMatch)
            {
                return null;
            }

            Context.Replies.Add(reply);
            return reply;
        }

        public void DeleteReply(Reply reply)
        {
            reply = reply
                    ?? throw new ArgumentNullException(nameof(reply));

            // 删除
            Context.Replies.Remove(reply);
        }


        public async Task<bool> SaveChangesAsync() =>
            await Context.SaveChangesAsync() > 0;

        public bool SaveChanges() =>
            Context.SaveChanges() > 0;
    }
}
