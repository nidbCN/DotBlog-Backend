using System;

namespace DotBlog.Server.Models
{
    public class ReplyDto
    {
        /// <summary>
        /// 回复ID
        /// </summary>
        public uint ReplyId { get; set; }

        /// <summary>
        /// 用户平台
        /// </summary>
        public string UserPlatform { get; set; }
        /// <summary>
        /// 用户浏览器
        /// </summary>
        public string UserExplore { get; set; }

        /// <summary>
        /// 头像网址
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 回复给某条评论ID
        /// </summary>
        public uint ReplyTo { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public uint Like { get; set; } = 0;

        /// <summary>
        /// 评论者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Mail { get; set; }

        /// <summary>
        /// 评论时间(JavaScript时间戳)
        /// </summary>
        public DateTime ReplyTime { get; set; }
    }
}
