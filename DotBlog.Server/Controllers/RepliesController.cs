using AutoMapper;
using DotBlog.Server.Dto;
using DotBlog.Server.Entities;
using DotBlog.Server.Services;
using DotBlog.Shared.Dto;
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
        private readonly IArticleService _articleService;

        /// <summary>
        /// 回复操作服务
        /// </summary>
        private readonly IReplyService _replyService;

        /// <summary>
        /// 日志记录服务
        /// </summary>
        private readonly ILogger<RepliesController> _logger;

        /// <summary>
        /// 对象映射服务
        /// </summary>
        private readonly IMapper _mapper;

        // 自定义字段
        private JsonSerializerOptions PrintOptions { get; }
            = new() { WriteIndented = true };

        // 构造函数
        public RepliesController(IArticleService articleService, IReplyService replyService, ILogger<RepliesController> logger, IMapper mapper)
        {
            _articleService = articleService;
            _replyService = replyService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// 获取回复
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 200</returns>
        [HttpGet(Name = nameof(GetReplies))]
        public async Task<ActionResult<ICollection<ReplyContentDto>>> GetReplies([FromRoute] uint articleId)
        {
            _logger.LogInformation($"Match method {nameof(GetReplies)}.");

            // 获取文章
            var article = await _articleService.GetArticleAsync(articleId);

            // 判断是否找到文章
            if (article == null)
            {
                _logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            // 获取回复
            var replies = await _replyService.GetRepliesAsync(article);

            // 返回评论Dto结果
            return Ok(
                _mapper.Map<ICollection<ReplyContentDto>>(replies)
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
            _logger.LogInformation($"Match method {nameof(UpdateReplyLike)}.");

            // 获取文章
            var article = await _articleService.GetArticleAsync(articleId);

            // 判断是否找到文章
            if (article == null)
            {
                _logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            // 获取回复
            var reply = await _replyService.GetReplyAsync(article, replyId);

            // 判断是否找到回复
            // ReSharper disable once InvertIf
            if (reply == null)
            {
                _logger.LogInformation("No reply was found, return a NotFound.");
                return NotFound();
            }

            // 更新回复点赞
            _replyService.UpdateReplyLike(reply);

            // ReSharper disable once InvertIf
            if (!await _replyService.SaveChangesAsync())
            {
                _logger.LogError("Cannot save changes, UnKnow error.");
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
            _logger.LogInformation($"Match method {nameof(CreateReply)}.");

            // 获取文章
            var article = await _articleService.GetArticleAsync(articleId);

            // 判断是否找到文章
            if (article == null)
            {
                _logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            _logger.LogDebug("Get input data:\n" + JsonSerializer.Serialize(inputReply, PrintOptions));

            // Dto映射为实体
            var reply = _mapper.Map<Reply>(inputReply);

            // 回复文章
            var result = _replyService.PostReply(article, reply);

            if (result == null)
            {
                return Accepted();
            }

            if (!await _replyService.SaveChangesAsync())
            {
                _logger.LogError("Cannot save changes, UnKnow error.");
            }

            // 返回结果
            var returnDto = _mapper.Map<ReplyContentDto>(result);
            return Created($"{Startup.ApiVersion}/articles/{articleId}/replies/{returnDto.ReplyId}",returnDto);
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="replyId">评论ID</param>
        /// <returns>HTTP 204 / HTTP 404</returns>
        //[Authorize]
        [HttpDelete("{replyId}")]
        public async Task<IActionResult> DeleteReply([FromRoute] uint articleId, [FromRoute] uint replyId)
        {
            _logger.LogInformation($"Match method {nameof(DeleteReply)}.");

            // 获取文章
            var article = await _articleService.GetArticleAsync(articleId);

            // 判断是否找到文章
            if (article == null)
            {
                _logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            var replyItem = await _replyService.GetReplyAsync(article, replyId);

            // 判断是否找到回复
            // ReSharper disable once InvertIf
            if (replyItem == null)
            {
                _logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            _replyService.DeleteReply(replyItem);

            // ReSharper disable once InvertIf
            if (!await _replyService.SaveChangesAsync())
            {
                _logger.LogError("Cannot save changes, UnKnow error.");
            }

            return NoContent();

        }

    }
}
