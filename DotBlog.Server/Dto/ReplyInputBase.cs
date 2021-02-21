using DotBlog.Shared.Dto;
using Masuit.Tools.Html;

namespace DotBlog.Server.Dto
{
    public class ReplyInputBase : ReplyBase
    {
        private string _author;
        private string _content;
        private string _link;
        private string _userExplore;
        private string _userPlatform;

        // 重写父类的字段
        public override string Author
        {
            get => _author;
            set => _author = value.HtmlSantinizerStandard();
        }

        public override string AvatarUrl { get; set; }

        public override string Content
        {
            get => _content;
            set => _content = value.HtmlSantinizerStandard();
        }

        public override string Link
        {
            get => _link;
            set => _link = value.HtmlSantinizerStandard();
        }

        public override string UserExplore
        {
            get => _userExplore;
            set => _userExplore = value.HtmlSantinizerStandard();
        }

        public override string UserPlatform
        {
            get => _userPlatform;
            set => _userPlatform = value.HtmlSantinizerStandard();
        }
    }
}
