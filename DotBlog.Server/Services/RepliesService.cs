using DotBlog.Server.Data;
using DotBlog.Server.Entities;
using DotBlog.Server.Repositories;
using Masuit.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotBlog.Server.Services
{
    public class RepliesService : IRepliesService
    {
        #region 私有字段

        private readonly IBlogsRepository _blogsRepository;

        private readonly ILogger<RepliesService> _logger;

        #endregion

        #region 构造函数

        public RepliesService(IBlogsRepository blogsRepository, ILogger<RepliesService> logger)
        {
            _blogsRepository = blogsRepository;
            _logger = logger;
        }

        #endregion

        #region 公有方法

        #region 获取相关

        public async Task<Reply?> GetAsync(uint articleId, uint replyId)
            => await _blogsRepository.GetReplyAsync((int)articleId, (int)replyId);

        public async Task<IList<Reply>?> GetAllAsync(uint articleId)
            => await _blogsRepository.GetAllRepliesAsync((int)articleId);

        #endregion

        #region 更新相关

        public void Like(Reply reply)
        {
            // 判空
            if (reply is null)
                throw new ArgumentNullException(nameof(reply));

            // 自增
            reply.Like++;
        }

        #endregion

        #region 新建相关

        public Reply Add(Article article, Reply reply)
        {
            if (article is null)
                throw new ArgumentNullException(nameof(article));

            if (reply is null)
                throw new ArgumentNullException(nameof(reply));

            // 新建回复
            reply.Article = article;
            reply.ArticleId = article.ArticleId;

            _blogsRepository.AddReply(reply);
            return reply;
        }

        #endregion

        public void Delete(Reply reply)
        {
            // 判空
            if (reply is null)
                throw new ArgumentNullException(nameof(reply));

            // 删除
            _blogsRepository.RemoveReply(reply);
        }

        #endregion
    }
}
