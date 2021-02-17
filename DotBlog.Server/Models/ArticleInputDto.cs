using Masuit.Tools.Html;

namespace DotBlog.Server.Models
{
    public abstract class ArticleInputDto : ArticleUniversalDto
    {
        /// <summary>
        /// 是否展示在首页上
        /// </summary>
        public bool IsShown { get; set; }

        private string _author;
        private string _category;
        private string _content;
        private string _description;
        private string _title;
        // 重写父类的字段

        public override string Author
        {
            get => _author;
            set => _author = value.HtmlSantinizerStandard();
        }

        public override string Category
        {
            get => _category;
            set => _category = value.HtmlSantinizerStandard();
        }

        public override string Content
        {
            get => _content;
            set => _content = value.HtmlSantinizerStandard();
        }

        public override string Description
        {
            get => _description;
            set => _description = value.HtmlSantinizerStandard();
        }

        public override string Title
        {
            get => _title;
            set => _title = value.HtmlSantinizerStandard();
        }
    }
}
