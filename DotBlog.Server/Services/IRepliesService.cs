using DotBlog.Server.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotBlog.Server.Services
{
    public interface IRepliesService
    {
        #region 获取相关

        /// <summary>
        /// 通过文章ID获取评论列表
        /// </summary>
        /// <param name="articleId">文章实体</param>
        /// <returns>回复实体列表</returns>
        public Task<IList<Reply>> GetAllAsync(uint articleId);


        /// <summary>
        /// 通过文章ID和回复ID获取回复
        /// </summary>
        /// <param name="articleId">文章实例</param>
        /// <param name="replyId">回复ID</param>
        /// <returns>回复实体</returns>
        public Task<Reply?> GetAsync(uint articleId, uint replyId);

        #endregion

        #region 更新相关

        /// <summary>
        /// 更新回复的点赞数
        /// </summary>
        /// <param name="reply">回复实体</param>
        /// <returns>更新结果</returns>
        public void Like(Reply reply);

        #endregion

        #region 写入相关

        /// <summary>
        /// 写入新评论
        /// </summary>
        /// <param name="article">文章实体</param>
        /// <param name="reply">新回复实体</param>
        /// <returns>更新的实体</returns>
        public Reply Add(Reply reply);

        #endregion

        #region 删除相关

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="reply">回复ID</param>
        /// <returns>删除结果</returns>
        public void Delete(Reply reply);

        #endregion

        #region 保存相关

        /// <summary>
        /// 保存更改
        /// </summary>
        /// <returns>保存结果</returns>
        public Task SaveChangesAsync();

        /// <summary>
        /// 保存更改
        /// </summary>
        /// <returns>保存结果</returns>
        void SaveChanges();

        #endregion
    }
}
