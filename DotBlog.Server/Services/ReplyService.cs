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

        private async Task<Reply> GetReply(Guid replyId) =>
            await Context.Replies
                .FirstOrDefaultAsync(it => it.ReplyId == replyId);

        /// <summary>
        /// 更新回复的点赞数
        /// </summary>
        /// <param name="replyId">Guid: 回复ID</param>
        /// <returns>uint: 更新后的点赞数</returns>
        public async Task<uint> PatchReplyLike(Guid replyId)
        {
            var reply = await GetReply(replyId);
            var ret = reply.Like++;
            if (await SaveChanges())
            {
                ret++;
            }

            return ret;
        }

        public async Task<bool> PostReply(Guid articleId, Reply reply)
        {
            var ret = false;

            reply.ReplyId = new Guid();
            reply.ReplyTime = DateTime.Now;

            if (reply.Link.MatchUrl() && reply.Mail.MatchEmail().isMatch)
            {
                await Context.Replies
                    .AddAsync(reply);
                ret = await SaveChanges();
            }

            return ret;
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
