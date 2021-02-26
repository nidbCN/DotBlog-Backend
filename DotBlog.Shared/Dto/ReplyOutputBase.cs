namespace DotBlog.Shared.Dto
{
    public abstract class ReplyOutputBase : ReplyBase
    {
        /// <summary>
        /// 回复ID
        /// </summary>
        public uint ReplyId { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public uint Like { get; set; }

        /// <summary>
        /// 评论时间(JavaScript时间戳)
        /// </summary>
        public string ReplyTime { get; set; }

        // 重写父类方法
        public override string Author { get; set; }
        public override string AvatarUrl { get; set; }
        public override string Content { get; set; }
        public override string Link { get; set; }
        public override string UserExplore { get; set; }
        public override string UserPlatform { get; set; }
    }
}
