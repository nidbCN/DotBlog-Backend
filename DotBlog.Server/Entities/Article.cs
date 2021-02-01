using System;
using System.Collections.Generic;

namespace DotBlog.Server.Entities
{
    public class Article
    {
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
        public bool IsShown { get; set; }

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

        /// <summary>
        /// 回复
        /// </summary>
        public List<Reply> Replies { get; set; }

        // /// <summary>
        // /// List[string]: 标签
        // /// </summary>
        // TODO(mail@gaein.cn)
        // public List<string> Tags { get; set; }
    }
}
