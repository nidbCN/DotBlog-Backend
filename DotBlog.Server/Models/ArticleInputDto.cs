using System;
using System.ComponentModel.DataAnnotations;

namespace DotBlog.Server.Models
{
    public class ArticleInputDto
    {
        /// <summary>
        /// 文章别名
        /// </summary>
        [Required]
        public string Alias { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 文章简介
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; } = "Anonymous";

        /// <summary>
        /// 文章内容
        /// </summary>
        [Required]
        public string Content { get; set; }

        // /// <summary>
        // /// List[string]: 标签
        // /// </summary>
        // TODO(mail@gaein.cn)
        // public List<string> Tags { get; set; }
    }
}
