using DotBlog.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotBlog.Server.Data
{
    public class DotBlogDbContext : DbContext
    {
        public DotBlogDbContext(DbContextOptions<DotBlogDbContext> options) : base(options)
        {
            // 构造函数，将options传给父类的构造函数
        }

        /// <summary>
        /// 文章表
        /// </summary>
        public DbSet<Article> Articles { get; set; }

        /// <summary>
        /// 回复表
        /// </summary>
        public DbSet<Reply> Replies { get; set; }

        // 重写父类方法设置数据库
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var articleConf = modelBuilder.Entity<Article>();
            var replyConf = modelBuilder.Entity<Reply>();

            // 设置关系
            replyConf
                .HasOne(it => it.Article)
                .WithMany(it => it.Replies)
                .HasForeignKey(it => it.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);

            // 设置键和索引
            replyConf.HasKey(it => it.ReplyId);
            replyConf.HasIndex(it => it.ReplyId);
            articleConf.HasKey(it => it.ArticleId);
            articleConf.HasIndex(it => it.ArticleId);

            articleConf.HasData(
                new Article()
                {
                    ArticleId = 1,
                    Title = "HelloWorld",
                    Description = "自动生成的第一篇文章",
                    Alias = "Hello-World",
                    Author = "DotBlog",
                    Content = "欢迎使用，这是DotBlog自动生成的第一篇文章",
                    IsShown = true
                }
            );
        }
    }
}
