using DotBlog.Shared.Dto;

namespace DotBlog.Blazor.Dto
{
    public class ReplyInputBase : ReplyBase
    {
        // 重写父类的字段
        public override string Author { get; set; }

        public override string AvatarUrl { get; set; }

        public override string Content { get; set; }

        public override string Link { get; set; }

        public override string UserExplore { get; set; }

        public override string UserPlatform { get; set; }
    }
}
