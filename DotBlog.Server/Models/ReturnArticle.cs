﻿using System;
using System.Collections.Generic;

using DotBlog.Server.Entities;

namespace DotBlog.Server.Models
{
    public class ReturnArticle
    {
        public static ReturnArticle Convert(Article articleItem)
        {
            if (articleItem == null)
            {
                throw new ArgumentNullException(nameof(articleItem));
            }

            var articleRetItem = new ReturnArticle()
            {
                ArticleId = articleItem.ArticleId,
                Category = articleItem.Category,
                Content = articleItem.Content,
                Description = articleItem.Description,
                Like = articleItem.Like,
                PostTime = articleItem.PostTime,
                Read = articleItem.Read,
                ResourceUri = articleItem.ResourceUri,
                // Tags = articleItem.Tags,
                Title = articleItem.Title
            };

            return articleRetItem;
        }

        /// <summary>
        /// 文章ID
        /// </summary>
        public Guid ArticleId { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文章简介
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 阅读数
        /// </summary>
        public uint Read { get; set; } = 0;

        /// <summary>
        /// 点赞数
        /// </summary>
        public uint Like { get; set; } = 0;

        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; }

        // /// <summary>
        // /// List[string]: 标签
        // /// </summary>
        // public List<string> Tags { get; set; }

        /// <summary>
        /// 发布时间(JavaScript时间戳)
        /// </summary>
        public DateTime PostTime { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 资源URI
        /// </summary>
        public string ResourceUri { get; set; }
    }
}
