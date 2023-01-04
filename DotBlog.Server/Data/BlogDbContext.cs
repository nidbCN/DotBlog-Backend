using DotBlog.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotBlog.Server.Data;

public class BlogDbContext : DbContext
{
#nullable disable

    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
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
            .HasOne(x => x.Article)
            .WithMany(x => x.Replies)
            .HasForeignKey(x => x.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        // 设置键和索引
        replyConf.Property(x => x.ArticleId).IsRequired();
        replyConf.Property(x => x.ReplyId).IsRequired();
        replyConf.HasKey(x => x.ReplyId);
        replyConf.HasIndex(x => x.ReplyId);

        articleConf.Property(x => x.ArticleId).IsRequired();
        articleConf.HasKey(x => x.ArticleId);
        articleConf.HasIndex(x => x.ArticleId);

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
#nullable restore
}
