using System.ComponentModel.DataAnnotations;

namespace DotBlog.Server.Models
{
    public abstract class ArticleUniversalDto
    {
        /// <summary>
        /// 文章别名
        /// </summary>
        [Display(Name = "文章别名")]
        [Required(ErrorMessage = "{0}这个字段是必填的")]
        public string Alias { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        [Display(Name = "文章标题")]
        [Required(ErrorMessage = "{0}这个字段是必填的")]
        public abstract string Title { get; set; }

        /// <summary>
        /// 文章简介
        /// </summary>
        [Display(Name = "文章简介")]
        [Required(ErrorMessage = "{0}这个字段是必填的")]
        public abstract string Description { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public abstract string Category { get; set; }

        
        /// <summary>
        /// 作者
        /// </summary>
        public abstract string Author { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        [Display(Name = "文章内容")]
        [Required(ErrorMessage = "{0}这个字段是必填的")]
        public abstract string Content { get; set; }
         
        // /// <summary>
        // /// List[string]: 标签
        // /// </summary>
        // TODO(mail@gaein.cn)
        // public List<string> Tags { get; set; }
    }
}
