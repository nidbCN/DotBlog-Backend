using System;

namespace DotBlog.Shared.Dto
{
    public abstract class ArticleOutputBase : ArticleBase
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public uint ArticleId { get; set; }

        /// <summary>
        /// 阅读数
        /// </summary>
        public uint Read { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public uint Like { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PostTime { get; set; }

        // 重写父类的字段

        public override string Author { get; set; }

        public override string Category { get; set; }

        public override string Content { get; set; }

        public override string Description { get; set; }

        public override string Title { get; set; }
    }
}
