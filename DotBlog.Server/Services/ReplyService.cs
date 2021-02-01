using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using DotBlog.Server.Data;
using DotBlog.Server.Entities;
using Masuit.Tools;

namespace DotBlog.Server.Services
{
    public class ReplyService : IReplyService
    {
        private DotBlogDbContext Context { get; }

        public ReplyService(DotBlogDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// 获取评论列表
        /// </summary>
        /// <param name="articleId">Guid: 文章ID</param>
        /// <returns>List[Reply]: 回复实例列表</returns>
        public async Task<List<Reply>> GetReplies(Guid articleId) =>
            await Context.Replies
                .Where(it => it.ArticleId == articleId)
                .ToListAsync();

        /// <summary>
        /// 获取回复
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="replyId">回复ID</param>
        /// <returns>Reply: 回复实例</returns>
        private async Task<Reply> GetReply(Guid articleId, Guid replyId)
        {
            var articleItem = await Context.Articles
                .FirstOrDefaultAsync(it => it.ArticleId == articleId);
            return articleItem.Replies
                .Find(it => it.ReplyId == replyId);
        }


        /// <summary>
        /// 更新回复的点赞数
        /// </summary>
        /// <param name="replyId">Guid: 回复ID</param>
        /// <param name="articleId">Guid: 文章ID</param>
        /// <returns>uint: 更新后的点赞数</returns>
        public async Task<bool> PatchReplyLike(Guid articleId, Guid replyId)
        {
            var replyItem = await GetReply(articleId, replyId);
            replyItem.Like++;
            return await SaveChanges();
        }

        public async Task<Reply> PostReply(Guid articleId, Reply reply)
        {
            var replyId = new Guid();
            reply.ReplyId = replyId;
            reply.ReplyTime = DateTime.Now;
            reply.ResourceUri = $"/article/{articleId}/reply/{replyId}";

            if (reply.Link.MatchUrl() && reply.Mail.MatchEmail().isMatch)
            {
                await Context.Replies
                    .AddAsync(reply);
                if (await SaveChanges())
                {
                    return reply;
                }
            }

            return null;
        }

        public async Task<bool> DeleteReply(Guid articleId, Guid replyId)
        {
            Context.Replies.Remove(
                await Context.Replies.FirstOrDefaultAsync(it => it.ArticleId == articleId)
            );
            return await SaveChanges();
        }

        private async Task<bool> SaveChanges() =>
            await Context.SaveChangesAsync() > 0;
    }
}
