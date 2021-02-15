using System;

namespace DotBlog.Server.Models
{
    public class ArticleListDto
    {
        /// <summary>
        /// 文章总数
        /// </summary>
        public uint Count { get; set; }

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
        public uint Read { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public uint Like { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PostTime { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        // /// <summary>
        // /// List[string]: 标签
        // /// </summary>
        // TODO(mail@gaein.cn)
        // public List<string> Tags { get; set; }
    }
}
