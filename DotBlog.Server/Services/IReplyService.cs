using DotBlog.Server.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotBlog.Server.Services
{
    public interface IReplyService
    {
        // 获取相关

        /// <summary>
        /// [异步]通过文章ID获取评论列表
        /// </summary>
        /// <param name="article">文章实体</param>
        /// <returns>回复实体列表</returns>
        Task<ICollection<Reply>> GetRepliesAsync(Article article);


        /// <summary>
        /// [异步]通过文章ID和回复ID获取回复
        /// </summary>
        /// <param name="article">文章实例</param>
        /// <param name="replyId">回复ID</param>
        /// <returns>回复实体</returns>
        Task<Reply> GetReplyAsync(Article article, uint replyId);

        // 更新相关

        /// <summary>
        /// 更新回复的点赞数
        /// </summary>
        /// <param name="reply">回复实体</param>
        /// <returns>更新结果</returns>
        void UpdateReplyLike(Reply reply);

        // 写入相关

        /// <summary>
        /// 写入新评论
        /// </summary>
        /// <param name="article">文章实体</param>
        /// <param name="reply">新回复实体</param>
        /// <returns>更新的实体</returns>
        Reply PostReply(Article article, Reply reply);

        // 删除相关

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="reply">回复ID</param>
        /// <returns>删除结果</returns>
        void DeleteReply(Reply reply);

        /// <summary>
        /// [异步]保存更改
        /// </summary>
        /// <returns>保存结果</returns>
        Task<bool> SaveChangesAsync();

        /// <summary>
        /// 保存更改
        /// </summary>
        /// <returns>保存结果</returns>
        bool SaveChanges();
    }
}
