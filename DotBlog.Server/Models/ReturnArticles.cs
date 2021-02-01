using System;
using System.Collections.Generic;

using DotBlog.Server.Entities;

namespace DotBlog.Server.Models
{
    public class ReturnArticles
    {
        public static List<ReturnArticles> Convert(List<Article> articles)
        {
            var articleRet = new List<ReturnArticles>();

            // TODO(mail@gaein.cn): 使用LINQ查询赋值
            foreach (var articleItem in articles)
            {
                var articleRetItem = new ReturnArticles()
                {
                    Alias = articleItem.Alias,
                    ArticleId = articleItem.ArticleId,
                    Description = articleItem.Description,
                    IsShown = articleItem.IsShown,
                    Like = articleItem.Like,
                    PostTime = articleItem.PostTime,
                    Read = articleItem.Read,
                    ResourceUri = articleItem.ResourceUri,
                    Title = articleItem.Title
                };
                articleRet.Add(articleRetItem);
            }

            return articleRet;
        }

        /// <summary>
        /// 文章ID
        /// </summary>
        public Guid ArticleId { get; set; }

        /// <summary>
        /// 文章别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文章简介
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否展示在首页上
        /// </summary>
        public bool IsShown { get; set; } = true;

        /// <summary>
        /// 阅读数
        /// </summary>
        public uint Read { get; set; } = 0;

        /// <summary>
        /// 点赞数
        /// </summary>
        public uint Like { get; set; } = 0;

        /// <summary>
        /// 发布时间(JavaScript时间戳)
        /// </summary>
        public DateTime PostTime { get; set; }

        /// <summary>
        /// 资源URI
        /// </summary>
        public string ResourceUri { get; set; }
    }
}
