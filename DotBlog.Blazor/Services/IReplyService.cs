using DotBlog.Blazor.Dto;
using DotBlog.Shared.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DotBlog.Blazor.Services
{
    public interface IReplyService
    {
        /// <summary>
        /// 获取评论
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <returns>评论列表</returns>
        Task<IList<ReplyContentDto>> GetReplyListAsync(uint articleId);

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <param name="reply">回复Id</param>
        /// <returns></returns>
        Task<ReplyContentDto> AddReplyAsync(uint articleId, ReplyAddDto reply);

        Task LikeReplyAsync(uint articleId, uint replyId);


    }
}
