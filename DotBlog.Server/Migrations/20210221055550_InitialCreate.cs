using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotBlog.Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ArticleId = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Alias = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsShown = table.Column<bool>(type: "INTEGER", nullable: false),
                    Read = table.Column<uint>(type: "INTEGER", nullable: false),
                    Like = table.Column<uint>(type: "INTEGER", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    PostTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: true),
                    Content = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleId);
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    ReplyId = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserPlatform = table.Column<string>(type: "TEXT", nullable: true),
                    UserExplore = table.Column<string>(type: "TEXT", nullable: true),
                    AvatarUrl = table.Column<string>(type: "TEXT", nullable: true),
                    ArticleId = table.Column<uint>(type: "INTEGER", nullable: false),
                    ReplyTo = table.Column<uint>(type: "INTEGER", nullable: false),
                    Like = table.Column<uint>(type: "INTEGER", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: true),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    Link = table.Column<string>(type: "TEXT", nullable: true),
                    Mail = table.Column<string>(type: "TEXT", nullable: true),
                    ReplyTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.ReplyId);
                    table.ForeignKey(
                        name: "FK_Replies_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "ArticleId", "Alias", "Author", "Category", "Content", "Description", "IsShown", "Like", "PostTime", "Read", "Title" },
                values: new object[] { 1u, "Hello-World", "DotBlog", null, "欢迎使用，这是DotBlog自动生成的第一篇文章", "自动生成的第一篇文章", true, 0u, new DateTime(2021, 2, 21, 13, 55, 50, 51, DateTimeKind.Local).AddTicks(9192), 0u, "HelloWorld" });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleId",
                table: "Articles",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_ArticleId",
                table: "Replies",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_ReplyId",
                table: "Replies",
                column: "ReplyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Replies");

            migrationBuilder.DropTable(
                name: "Articles");
        }
    }
}
