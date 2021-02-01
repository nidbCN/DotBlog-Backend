using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DotBlog.Server.Entities;

namespace DotBlog.Server.Services
{
    public interface IReplyService
    {
        // 获取相关

        /// <summary>
        /// 获取评论列表
        /// </summary>
        /// <param name="articleId">Guid: 文章ID</param>
        /// <returns>List[Reply]: 回复实例列表</returns>
        Task<List<Reply>> GetReplies(Guid articleId);

        // 更新相关

        /// <summary>
        /// 更新回复的点赞数
        /// </summary>
        /// <param name="articleId">Guid: 文章ID</param>
        /// <param name="replyId">Guid: 回复ID</param>
        /// <returns>uint: 更新后的点赞数</returns>
        Task<bool> PatchReplyLike(Guid articleId, Guid replyId);

        // 写入相关

        /// <summary>
        /// 写入新评论
        /// </summary>
        /// <param name="articleId">Guid: 回复的文章ID</param>
        /// <param name="reply">ReplyItem: 新评论</param>
        /// <returns></returns>
        Task<Reply> PostReply(Guid articleId, Reply reply);

        // 删除相关

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="articleId">Guid: 文章ID</param>
        /// <param name="replyId">Guid: 回复ID</param>
        /// <returns></returns>
        Task<bool> DeleteReply(Guid articleId, Guid replyId);
    }
}
