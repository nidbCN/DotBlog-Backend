using System;
using System.Diagnostics.CodeAnalysis;

namespace DotBlog.Server.Entities;

public class Reply
{

#nullable disable
    public Article Article { get; set; }
#nullable restore

    /// <summary>
    /// 用户平台
    /// </summary>
    public string? UserPlatform { get; set; }

    /// <summary>
    /// 用户浏览器
    /// </summary>
    public string? UserExplore { get; set; }

    /// <summary>
    /// 头像网址
    /// </summary>
    public string? AvatarUrl { get; set; }

    /// <summary>
    /// 评论的文章ID
    /// </summary>
    [NotNull]
    public int ArticleId { get; set; }

    /// <summary>
    /// 某条评论的ID
    /// </summary>
    [NotNull]
    public int ReplyId { get; set; }

    /// <summary>
    /// 回复给某条评论ID
    /// </summary>
    public int ReplyTo { get; set; }

    /// <summary>
    /// 点赞数
    /// </summary>
    public int Like { get; set; } = 0;

    /// <summary>
    /// 评论者
    /// </summary>
    public string Author { get; set; } = "匿名";

    /// <summary>
    /// 评论内容
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 链接
    /// </summary>
    public string? Link { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Mail { get; set; }

    /// <summary>
    /// 评论时间
    /// </summary>
    public DateTime ReplyTime { get; set; } = DateTime.Now;
}
