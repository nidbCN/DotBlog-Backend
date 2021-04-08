﻿// <auto-generated />
using System;
using DotBlog.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DotBlog.Server.Migrations
{
    [DbContext(typeof(DotBlogDbContext))]
    partial class DotBlogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("DotBlog.Server.Entities.Account", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

                    b.Property<string>("Mail")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.HasIndex("Name");

                    b.HasIndex("Phone");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("DotBlog.Server.Entities.Article", b =>
                {
                    b.Property<long>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

                    b.Property<string>("Alias")
                        .HasColumnType("text");

                    b.Property<string>("Author")
                        .HasColumnType("text");

                    b.Property<string>("Category")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsShown")
                        .HasColumnType("boolean");

                    b.Property<long>("Like")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("PostTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("Read")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("ArticleId");

                    b.HasIndex("ArticleId");

                    b.ToTable("Articles");

                    b.HasData(
                        new
                        {
                            ArticleId = 1L,
                            Alias = "Hello-World",
                            Author = "DotBlog",
                            Content = "欢迎使用，这是DotBlog自动生成的第一篇文章",
                            Description = "自动生成的第一篇文章",
                            IsShown = true,
                            Like = 0L,
                            PostTime = new DateTime(2021, 4, 8, 21, 31, 31, 659, DateTimeKind.Local).AddTicks(9652),
                            Read = 0L,
                            Title = "HelloWorld"
                        });
                });

            modelBuilder.Entity("DotBlog.Server.Entities.Reply", b =>
                {
                    b.Property<long>("ReplyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

                    b.Property<long>("ArticleId")
                        .HasColumnType("bigint");

                    b.Property<string>("Author")
                        .HasColumnType("text");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<long>("Like")
                        .HasColumnType("bigint");

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.Property<string>("Mail")
                        .HasColumnType("text");

                    b.Property<DateTime>("ReplyTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("ReplyTo")
                        .HasColumnType("bigint");

                    b.Property<string>("UserExplore")
                        .HasColumnType("text");

                    b.Property<string>("UserPlatform")
                        .HasColumnType("text");

                    b.HasKey("ReplyId");

                    b.HasIndex("ArticleId");

                    b.HasIndex("ReplyId");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("DotBlog.Server.Entities.Reply", b =>
                {
                    b.HasOne("DotBlog.Server.Entities.Article", "Article")
                        .WithMany("Replies")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");
                });

            modelBuilder.Entity("DotBlog.Server.Entities.Article", b =>
                {
                    b.Navigation("Replies");
                });
#pragma warning restore 612, 618
        }
    }
}
