using DotBlog.Server.Dto.QueryModel;
using DotBlog.Server.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotBlog.Server.Services;

public interface IArticlesService
{
    #region 获取相关
    /// <summary>
    /// 获取文章列表
    /// </summary>
    /// <returns>文章列表</returns>
    public Task<IList<Article>> GetAllAsync();

    /// <summary>
    /// 获取查询文章列表
    /// </summary>
    /// <param name="page">页码</param>
    /// <param name="limit">数量</param>
    /// <returns>文章列表</returns>
    public Task<IList<Article>> GetAllAsync(ArticleGetDtoParameters param);

    /// <summary>
    /// 通过文章ID获得文章实例
    /// </summary>
    /// <param name="articleId">文章ID</param>
    /// <returns>文章实例</returns>
    public Task<Article?> GetAsync(uint articleId);

    #endregion

    #region 更新相关

    /// <summary>
    /// 更新文章的点赞数
    /// </summary>
    /// <param name="article">文章实体</param>
    /// <returns>更新结果</returns>
    public void Like(Article article);

    /// <summary>
    /// 更新文章已读数
    /// </summary>
    /// <param name="article">文章实体</param>
    /// <returns>更新结果</returns>
    public void Read(Article article);

    #endregion

    #region 写入相关

    /// <summary>
    /// 写入新文章
    /// </summary>
    /// <param name="article">新文章实体</param>
    /// <returns>保存结果</returns>
    public Article Add(Article article);

    #endregion

    #region 删除相关

    /// <summary>
    /// 删除文章
    /// </summary>
    /// <param name="article">要删除的文章实体</param>
    /// <returns>删除结果</returns>
    public void Remove(Article article);

    #endregion

    #region 保存相关

    /// <summary>
    /// 保存
    /// </summary>
    /// <returns></returns>
    public Task SaveAsync();

    /// <summary>
    /// 保存
    /// </summary>
    public void Save();

    #endregion
}
