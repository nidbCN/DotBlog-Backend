using DotBlog.Server.Dto.QueryModel;
using DotBlog.Server.Entities;
using DotBlog.Server.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotBlog.Server.Services
{
    public class ArticlesService : IArticlesService
    {
        #region 私有字段
        /// <summary>
        /// 文章存储服务
        /// </summary>
        private readonly IBlogsRepository _blogsRepository;

        /// <summary>
        /// 日志服务
        /// </summary>
        private readonly ILogger<ArticlesService> _logger;
        #endregion

        #region 构造函数
        public ArticlesService(IBlogsRepository articlesRepository, ILogger<ArticlesService> logger)
        {
            _blogsRepository = articlesRepository;
            _logger = logger;
        }
        #endregion

        #region 获取相关

        public async Task<IList<Article>> GetAllAsync()
            => await _blogsRepository.GetAllArticlesAsync();

        public async Task<IList<Article>> GetAllAsync(ArticleGetDtoParameters param)
        {
            if (param is null)
                return await GetAllAsync();

            bool match(Article x) => x.Category == param.Category;

            return await _blogsRepository.GetMatchedArticlesAsync(match,(int)param.Page,(int)param.Size);
        }

        public async Task<Article?> GetAsync(uint articleId)
            => await _blogsRepository.GetArticleAsync((int)articleId);

        #endregion

        #region 更新相关

        public void Like(Article article)
        {
            // 判空
            article = article
                      ?? throw new ArgumentNullException(nameof(article));

            // 自增
            article.Like++;
        }

        public void Read(Article article)
        {
            // 判空
            article = article
                      ?? throw new ArgumentNullException(nameof(article));

            // 自增
            article.Read++;
        }

        #endregion

        #region 写入相关

        public Article Add(Article article)
        {
            // 判空
            if (article is null)
                throw new ArgumentNullException(nameof(article));

            // 添加文章
            _blogsRepository.AddArticle(article);
            // 返回结果
            return article;
        }

        #endregion

        #region 删除相关

        public void Remove(Article article)
        {
            if (article is null)
                throw new ArgumentNullException(nameof(article));

            _blogsRepository.RemoveArticle(article);
        }

        #endregion

        #region 保存相关

        public async Task SaveAsync() =>
            await _blogsRepository.SaveAsync();

        public void Save() =>
            _blogsRepository.Save();

        #endregion
    }
}
