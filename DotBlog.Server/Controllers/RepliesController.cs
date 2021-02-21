using AutoMapper;
using DotBlog.Server.Dto;
using DotBlog.Server.Entities;
using DotBlog.Server.Services;
using DotBlog.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

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
        public async Task<ActionResult<ICollection<ReplyContentDto>>> GetReplies([FromRoute] uint articleId)
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
            var replies = await ReplyService.GetRepliesAsync(article);

            // 返回评论Dto结果
            return Ok(
                Mapper.Map<ICollection<ReplyContentDto>>(replies)
            );
        }

        /// <summary>
        /// 更新回复点赞
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="replyId">回复ID</param>
        /// <returns>HTTP 200 / HTTP 204 / HTTP 400</returns>
        [HttpPost("{replyId}/Like")]
        public async Task<IActionResult> UpdateReplyLike([FromRoute] uint articleId, [FromRoute] uint replyId)
        {
            Logger.LogInformation($"Match method {nameof(UpdateReplyLike)}.");

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
            }

            // 返回结果
            return NoContent();
        }

        /// <summary>
        /// 新建回复
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="inputReply">回复</param>
        /// <returns>HTTP 201 / HTTP 202 / HTTP 400</returns>
        [HttpPost]
        public async Task<ActionResult<ReplyContentDto>> CreateReply([FromRoute] uint articleId, [FromBody] ReplyAddDto inputReply)
        {
            Logger.LogInformation($"Match method {nameof(CreateReply)}.");

            // 获取文章
            var article = await ArticleService.GetArticleAsync(articleId);

            // 判断是否找到文章
            if (article == null)
            {
                Logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

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
            }

            // 返回结果
            var returnDto = Mapper.Map<ReplyContentDto>(result);
            return Created($"{Startup.ApiVersion}/articles/{articleId}/replies/{returnDto.ReplyId}",returnDto);
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="replyId">评论ID</param>
        /// <returns>HTTP 204 / HTTP 404</returns>
        [Authorize]
        [HttpDelete("{replyId}")]
        public async Task<IActionResult> DeleteReply([FromRoute] uint articleId, [FromRoute] uint replyId)
        {
            Logger.LogInformation($"Match method {nameof(DeleteReply)}.");

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
            }

            return NoContent();

        }

    }
}
