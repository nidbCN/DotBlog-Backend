using System;
using System.Collections.Generic;
using System.Linq;

using Masuit.Tools;
using DotBlog.Server.Data;
using DotBlog.Server.Entities;

namespace DotBlog.Server.Services
{
    public class ReplyService : IReplyService
    {
        private DotBlogDbContext Context { get; }

        public ReplyService(DotBlogDbContext context)
        {
            Context = context;
        }

        public Reply GetReply(Article articleItem, Guid replyId) =>
            articleItem?.Replies?
                .FirstOrDefault(it => it.ReplyId == replyId);

        public ICollection<Reply> GetReplies(Article articleItem)
        {
            // 判空
            if (articleItem == null)
            {
                throw new ArgumentNullException(nameof(articleItem));
            }

            return articleItem.Replies;
        }

        public bool PatchReplyLike(Article articleItem, Reply replyItem)
        {
            // 判空
            if (articleItem == null)
            {
                throw new ArgumentNullException(nameof(articleItem));
            }
            if (replyItem == null)
            {
                throw new ArgumentNullException(nameof(replyItem));
            }

            // 自增
            replyItem.Like++;
            return SaveChanges();
        }

        public Reply PostReply(Article articleItem, Reply reply)
        {
            // 判空
            if (articleItem == null)
            {
                throw new ArgumentNullException(nameof(articleItem));
            }
            if (reply == null)
            {
                throw new ArgumentNullException(nameof(reply));
            }

            // 新建回复
            var replyId = Guid.NewGuid();
            reply.ReplyId = replyId;
            reply.ArticleId = articleItem.ArticleId;
            reply.ReplyTime = DateTime.Now;
            reply.ResourceUri = $"/article/{articleItem.ArticleId}/reply/{replyId}";

            if (!reply.Link.MatchUrl() || !reply.Mail.MatchEmail().isMatch)
            {
                return null;
            }

            Context.Replies.Add(reply);
            SaveChanges();
            return reply;
        }

        public bool DeleteReply(Article articleItem, Reply replyItem)
        {
            // 删除
            Context.Replies.Remove(replyItem);
            return SaveChanges();
        }

        /// <summary>
        /// 保存更改
        /// </summary>
        /// <returns>保存结果</returns>
        private bool SaveChanges() =>
            Context.SaveChanges() > 0;
    }
}
