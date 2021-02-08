using Microsoft.EntityFrameworkCore;

using DotBlog.Server.Entities;

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
            // 设置对应关系
            
            modelBuilder.Entity<Reply>()
                .HasOne(it => it.Article)
                .WithMany(it=>it.Replies)
                .HasForeignKey(it => it.ArticleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Article>()
                .HasOne(it => it.Category)
                .WithMany(it => it.Articles)
                .HasForeignKey(it => it.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
