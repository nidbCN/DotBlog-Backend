using DotBlog.Server.Entities;
using DotBlog.Server.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotBlog.Server.Services;

public class RepliesService : IRepliesService
{
    #region 私有字段

    private readonly IBlogsRepository _blogsRepo;

    private readonly ILogger<RepliesService> _logger;

    #endregion

    #region 构造函数

    public RepliesService(IBlogsRepository blogsRepo, ILogger<RepliesService> logger)
    {
        _blogsRepo = blogsRepo;
        _logger = logger;
    }

    #endregion

    #region 公有方法

    #region 获取相关

    public async Task<Reply?> GetAsync(uint articleId, uint replyId)
        => await _blogsRepo.GetReplyAsync((int)articleId, (int)replyId);

    public async Task<IList<Reply>?> GetAllAsync(uint articleId)
        => await _blogsRepo.GetAllRepliesAsync((int)articleId);

    #endregion

    #region 更新相关

    public async void Like(Reply reply)
    {
        // 判空
        if (reply is null)
            throw new ArgumentNullException(nameof(reply));

        // 自增
        reply.Like++;

        await _blogsRepo.SaveAsync();
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

        _blogsRepo.AddReply(reply);

        _blogsRepo.Save();
        return reply;
    }

    #endregion

    public void Delete(Reply reply)
    {
        // 判空
        if (reply is null)
            throw new ArgumentNullException(nameof(reply));

        // 删除
        _blogsRepo.RemoveReply(reply);
    }

    #endregion
}
