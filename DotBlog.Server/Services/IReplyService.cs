using System.Collections.Generic;

using DotBlog.Server.Entities;

namespace DotBlog.Server.Services
{
    public interface IReplyService
    {
        // 获取相关

        /// <summary>
        /// 通过文章ID获取评论列表
        /// </summary>
        /// <param name="articleItem">文章实体</param>
        /// <returns>回复实体列表</returns>
        ICollection<Reply> GetReplies(Article articleItem);

        ///// <summary>
        ///// 通过文章ID异步获取评论列表
        ///// </summary>
        ///// <param name="articleItem">文章实体</param>
        ///// <returns>回复实体列表</returns>
        //Task<ICollection<Reply>> GetReplies(Article articleItem);

        /// <summary>
        /// 通过文章ID和回复ID获取回复
        /// </summary>
        /// <param name="articleItem">文章实例</param>
        /// <param name="replyId">回复ID</param>
        /// <returns>回复实体</returns>
        Reply GetReply(Article articleItem, uint replyId);


        ///// <summary>
        ///// 通过文章ID和回复ID异步获取回复
        ///// </summary>
        ///// <param name="articleItem">文章实例</param>
        ///// <param name="replyId">回复ID</param>
        ///// <returns>回复实体</returns>
        //Task<Reply> GetReplyAsync(Article articleItem, uint replyId);



        // 更新相关

        /// <summary>
        /// 更新回复的点赞数
        /// </summary>
        /// <param name="articleItem">文章实体</param>
        /// <param name="replyItem">回复实体</param>
        /// <returns>更新结果</returns>
        bool PatchReplyLike(Article articleItem, Reply replyItem);

        // 写入相关

        /// <summary>
        /// 写入新评论
        /// </summary>
        /// <param name="articleItem">文章实体</param>
        /// <param name="replyItem">新回复实体</param>
        /// <returns>更新的实体</returns>
        Reply PostReply(Article articleItem, Reply replyItem);

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
