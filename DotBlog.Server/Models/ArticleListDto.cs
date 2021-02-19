namespace DotBlog.Server.Models
{
    public class ArticleListDto : ArticleOutputBase
    {
        /// <summary>
        /// 是否展示在首页上
        /// </summary>
        public bool IsShown { get; set; }
    }
}
