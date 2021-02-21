using System.ComponentModel.DataAnnotations;

namespace DotBlog.Shared.Dto
{
    public class ArticleContentDto : ArticleOutputBase
    {
        /// <summary>
        /// 文章内容
        /// </summary>
        [Display(Name = "文章内容")]
        [Required(ErrorMessage = "{0}这个字段是必填的")]
        public string Content { get; set; }
    }
}
