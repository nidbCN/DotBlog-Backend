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
    [Route(Startup.ApiVersion + "Articles/{articleId}/[controller]")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        // 通过DI注入的只读服务
        private IArticleService ArticleService { get; }
        private IReplyService ReplyService { get; }
        private ILogger<RepliesController> Logger { get; }
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
        [HttpGet]
        public async Task<ActionResult<ICollection<ReplyDto>>> GetReplies([FromRoute] uint articleId)
        {
            Logger.LogInformation($"Match method {nameof(GetReplies)}.");

            // 获取文章
            var articleItem = await ArticleService.GetArticleAsync(articleId);
            // 判空
            if (articleItem == null)
            {
                Logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }
            // 返回评论列表
            return Ok(
                Mapper.Map<ICollection<ReplyDto>>(ReplyService.GetReplies(articleItem))
            );
        }

        /// <summary>
        /// 更新回复点赞
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="replyId">回复ID</param>
        /// <returns>HTTP 200 / HTTP 204 / HTTP 400</returns>
        [HttpPatch("{replyId}/Like")]
        public IActionResult PatchReplyLike([FromRoute] uint articleId, [FromRoute] uint replyId)
        {
            Logger.LogInformation($"Match method {nameof(PatchReplyLike)}.");

            var articleItem = ArticleService.GetArticle(articleId);
            if (articleItem == null)
            {
                Logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            var replyItem = ReplyService.GetReply(articleItem, replyId);

            if (replyItem == null)
            {
                Logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            return ReplyService.PatchReplyLike(articleItem, replyItem)
                ? ResetContent()
                : InternalServerError();
        }

        /// <summary>
        /// 新建回复
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="replyItemDto">回复</param>
        /// <returns>HTTP 201 / HTTP 202 / HTTP 400</returns>
        [HttpPost]
        public ActionResult<ReplyDto> PostReply([FromRoute] uint articleId, [FromBody] ReplyDto replyItemDto)
        {
            Logger.LogInformation($"Match method {nameof(PostReply)}.");

            var articleItem = ArticleService.GetArticle(articleId);
            if (articleItem == null)
            {
                Logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            replyItemDto.Content?.HtmlSantinizerStandard();
            replyItemDto.Author?.HtmlSantinizerStandard();
            replyItemDto.AvatarUrl?.HtmlSantinizerStandard();
            replyItemDto.Link?.HtmlSantinizerStandard();
            replyItemDto.Mail?.HtmlSantinizerStandard();
            replyItemDto.UserExplore?.HtmlSantinizerStandard();
            replyItemDto.UserExplore?.HtmlSantinizerStandard();

            Logger.LogInformation(JsonSerializer.Serialize(replyItemDto, PrintOptions));

            var replyItem = Mapper.Map<Reply>(replyItemDto);

            var result = ReplyService.PostReply(articleItem, replyItem);
            return result == null
                ? Accepted()
                : Created(result.ResourceUri, result);
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="replyId">评论ID</param>
        /// <returns></returns>
        //[Authorize]
        [HttpDelete("{replyId}")]
        public IActionResult DeleteReply([FromRoute] uint articleId, [FromRoute] uint replyId)
        {
            Logger.LogInformation($"Match method {nameof(DeleteReply)}.");

            var articleItem = ArticleService.GetArticle(articleId);
            if (articleItem == null)
            {
                Logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            var replyItem = ReplyService.GetReply(articleItem, replyId);

            if (replyItem == null)
            {
                Logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            return ReplyService.DeleteReply(articleItem, replyItem)
                ? ResetContent()
                : InternalServerError();

        }

    }
}
