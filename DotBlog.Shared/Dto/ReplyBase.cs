﻿using System.ComponentModel.DataAnnotations;

namespace DotBlog.Shared.Dto;

public abstract class ReplyBase
{
    /// <summary>
    /// 用户平台
    /// </summary>
    public abstract string UserPlatform { get; set; }

    /// <summary>
    /// 用户浏览器
    /// </summary>
    public abstract string UserBrowser { get; set; }

    /// <summary>
    /// 头像网址
    /// </summary>
    public abstract string AvatarUrl { get; set; }

    /// <summary>
    /// 回复给某条评论ID
    /// </summary>
    public uint ReplyTo { get; set; }

    /// <summary>
    /// 评论者
    /// </summary>
    public abstract string Author { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    [Required]
    public abstract string Content { get; set; }

    /// <summary>
    /// 链接
    /// </summary>
    [Url]
    public abstract string Link { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [EmailAddress]
    public string Mail { get; set; }

}
