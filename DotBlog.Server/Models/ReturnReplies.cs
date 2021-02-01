using System;
using System.Collections.Generic;

using DotBlog.Server.Entities;

namespace DotBlog.Server.Models
{
    public class ReturnReplies
    {
        public static List<ReturnReplies> Convert(List<Reply> replies)
        {
            var replyRet = new List<ReturnReplies>();

            // TODO(mail@gaein.cn): 使用LINQ查询赋值
            foreach (var replyItem in replies)
            {
                var replyRetItem = new ReturnReplies()
                {
                    ArticleId = replyItem.ArticleId,
                    Author = replyItem.Author,
                    AvatarUrl = replyItem.AvatarUrl,
                    Content = replyItem.Content,
                    Like = replyItem.Like,
                    Link = replyItem.Link,
                    Mail = replyItem.Mail,
                    ReplyId = replyItem.ReplyId,
                    ReplyTime = replyItem.ReplyTime,
                    ReplyTo = replyItem.ReplyTo,
                    ResourceUri = replyItem.Article.ResourceUri + $"/reply/{replyItem.ReplyId}",
                    UserExplore = replyItem.UserExplore,
                    UserPlatform = replyItem.UserPlatform
                };
                replyRet.Add(replyRetItem);
            }

            return replyRet;
        }

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
        /// 评论的文章ID
        /// </summary>
        public Guid ArticleId { get; set; }

        /// <summary>
        /// 某条评论的ID
        /// </summary>
        public Guid ReplyId { get; set; }

        /// <summary>
        /// 回复给某条评论ID
        /// </summary>
        public Guid ReplyTo { get; set; }

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

        /// <summary>
        /// 资源URI
        /// </summary>
        public string ResourceUri { get; set; }
    }
}
