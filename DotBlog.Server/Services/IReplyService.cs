using System;
using System.Collections.Generic;

using DotBlog.Server.Entities;

namespace DotBlog.Server.Services
{
    public interface IReplyService
    {
        // 获取相关

        /// <summary>
        /// 获取回复
        /// </summary>
        /// <param name="articleItem">文章实例</param>
        /// <param name="replyId">回复ID</param>
        /// <returns>回复实例</returns>
        Reply GetReply(Article articleItem, Guid replyId);

        /// <summary>
        /// 获取评论列表
        /// </summary>
        /// <param name="articleItem">文章实体</param>
        /// <returns>回复实例列表</returns>
        ICollection<Reply> GetReplies(Article articleItem);

        // 更新相关

        /// <summary>
        /// 更新回复的点赞数
        /// </summary>
        /// <param name="articleItem">文章实体</param>
        /// <param name="replyItem">回复ID</param>
        /// <returns>更新结果</returns>
        bool PatchReplyLike(Article articleItem, Reply replyItem);

        // 写入相关

        /// <summary>
        /// 写入新评论
        /// </summary>
        /// <param name="articleItem">文章实体</param>
        /// <param name="reply">新评论实体</param>
        /// <returns>更新的实体</returns>
        Reply PostReply(Article articleItem, Reply reply);

        // 删除相关

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="articleItem">文章实体</param>
        /// <param name="replyItem">回复ID</param>
        /// <returns>删除结果</returns>
        bool DeleteReply(Article articleItem, Reply replyItem);
    }
}
