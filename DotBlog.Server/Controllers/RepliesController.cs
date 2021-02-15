using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using AutoMapper;
using Masuit.Tools.Html;
using DotBlog.Server.Entities;
using DotBlog.Server.Models;
using DotBlog.Server.Services;

namespace DotBlog.Server.Controllers
{
    [Route(Startup.ApiVersion + "/Articles/{articleId}/[controller]")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        // 通过DI注入的只读服务

        /// <summary>
        /// 文章操作服务
        /// </summary>
        private IArticleService ArticleService { get; }

        /// <summary>
        /// 回复操作服务
        /// </summary>
        private IReplyService ReplyService { get; }

        /// <summary>
        /// 日志记录服务
        /// </summary>
        private ILogger<RepliesController> Logger { get; }

        /// <summary>
        /// 对象映射服务
        /// </summary>
        private IMapper Mapper { get; }

        // 自定义返回类型
        private StatusCodeResult ResetContent() => StatusCode(205);
        private StatusCodeResult InternalServerError() => StatusCode(500);

        // 自定义字段
        private JsonSerializerOptions PrintOptions { get; }
            = new() { WriteIndented = true };

        // 构造函数
        public RepliesController(IArticleService articleService, IReplyService replyService, ILogger<RepliesController> logger, IMapper mapper)
        {
            ArticleService = articleService;
            ReplyService = replyService;
            Logger = logger;
            Mapper = mapper;
        }

        /// <summary>
        /// 获取回复
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 200</returns>
        [HttpGet(Name = nameof(GetReplies))]
        public async Task<ActionResult<ICollection<ReplyDto>>> GetReplies([FromRoute] uint articleId)
        {
            Logger.LogInformation($"Match method {nameof(GetReplies)}.");

            // 获取文章
            var article = await ArticleService.GetArticleAsync(articleId);

            // 判断是否找到文章
            if (article == null)
            {
                Logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            // 获取回复
            var replies = ReplyService.GetReplies(article);

            // 返回评论Dto结果
            return Ok(
                Mapper.Map<ICollection<ReplyDto>>(replies)
            );
        }

        /// <summary>
        /// 更新回复点赞
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="replyId">回复ID</param>
        /// <returns>HTTP 200 / HTTP 204 / HTTP 400</returns>
        [HttpPatch("{replyId}/Like")]
        public async Task<IActionResult> PatchReplyLikeAsync([FromRoute] uint articleId, [FromRoute] uint replyId)
        {
            Logger.LogInformation($"Match method {nameof(PatchReplyLikeAsync)}.");

            // 获取文章
            var article = await ArticleService.GetArticleAsync(articleId);

            // 判断是否找到文章
            if (article == null)
            {
                Logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            // 获取回复
            var reply = await ReplyService.GetReplyAsync(article, replyId);

            // 判断是否找到回复
            // ReSharper disable once InvertIf
            if (reply == null)
            {
                Logger.LogInformation("No reply was found, return a NotFound.");
                return NotFound();
            }

            // 更新回复点赞
            ReplyService.PatchReplyLike(reply);

            // ReSharper disable once InvertIf
            if (!await ReplyService.SaveChangesAsync())
            {
                Logger.LogError("Cannot save changes, UnKnow error.");
                return InternalServerError();
            }

            // 返回结果
            return ResetContent();
        }

        /// <summary>
        /// 新建回复
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="inputReply">回复</param>
        /// <returns>HTTP 201 / HTTP 202 / HTTP 400</returns>
        [HttpPost]
        public async Task<ActionResult<ReplyDto>> PostReplyAsync([FromRoute] uint articleId, [FromBody] ReplyInputDto inputReply)
        {
            Logger.LogInformation($"Match method {nameof(PostReplyAsync)}.");

            // 获取文章
            var article = await ArticleService.GetArticleAsync(articleId);

            // 判断是否找到文章
            if (article == null)
            {
                Logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            // 安全检查
            inputReply.Content?.HtmlSantinizerStandard();
            inputReply.Author?.HtmlSantinizerStandard();
            inputReply.AvatarUrl?.HtmlSantinizerStandard();
            inputReply.Link?.HtmlSantinizerStandard();
            inputReply.Mail?.HtmlSantinizerStandard();
            inputReply.UserExplore?.HtmlSantinizerStandard();
            inputReply.UserExplore?.HtmlSantinizerStandard();

            Logger.LogDebug("Get input data:\n" + JsonSerializer.Serialize(inputReply, PrintOptions));

            // Dto映射为实体
            var reply = Mapper.Map<Reply>(inputReply);

            // 回复文章
            var result = ReplyService.PostReply(article, reply);

            if (result == null)
            {
                return Accepted();
            }

            if (!await ReplyService.SaveChangesAsync())
            {
                Logger.LogError("Cannot save changes, UnKnow error.");
                return InternalServerError();
            }

            // 返回结果
            var returnDto = Mapper.Map<ReplyDto>(result);
            return CreatedAtRoute(nameof(GetReplies), new { replyId = returnDto.ReplyId }, returnDto);
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="replyId">评论ID</param>
        /// <returns></returns>
        //[Authorize]
        [HttpDelete("{replyId}")]
        public async Task<IActionResult> DeleteReplyAsync([FromRoute] uint articleId, [FromRoute] uint replyId)
        {
            Logger.LogInformation($"Match method {nameof(DeleteReplyAsync)}.");

            // 获取文章
            var article = await ArticleService.GetArticleAsync(articleId);

            // 判断是否找到文章
            if (article == null)
            {
                Logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            var replyItem = await ReplyService.GetReplyAsync(article, replyId);

            // 判断是否找到回复
            // ReSharper disable once InvertIf
            if (replyItem == null)
            {
                Logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            ReplyService.DeleteReply(replyItem);

            // ReSharper disable once InvertIf
            if (!await ReplyService.SaveChangesAsync())
            {
                Logger.LogError("Cannot save changes, UnKnow error.");
                return InternalServerError();
            }

            return ResetContent();

        }

    }
}
