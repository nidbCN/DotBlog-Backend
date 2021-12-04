using System;
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
    [Route("v1/Articles/{articleId}/[controller]")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        // 通过DI注入的只读服务

        /// <summary>
        /// 文章操作服务
        /// </summary>
        private readonly IArticlesService _articleService;

        /// <summary>
        /// 回复操作服务
        /// </summary>
        private readonly IRepliesService _replyService;

        /// <summary>
        /// 日志记录服务
        /// </summary>
        private readonly ILogger<RepliesController> _logger;

        /// <summary>
        /// 对象映射服务
        /// </summary>
        private readonly IMapper _mapper;

        // 自定义字段
        private readonly JsonSerializerOptions _printOptions = new() { WriteIndented = true };

        // 构造函数
        public RepliesController(IArticlesService articleService, IRepliesService replyService, ILogger<RepliesController> logger, IMapper mapper)
        {
            _articleService = articleService ??
                              throw new ArgumentNullException(nameof(articleService));
            _replyService = replyService ??
                            throw new ArgumentNullException(nameof(replyService));
            _logger = logger ??
                      throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// 获取回复
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 200</returns>
        [HttpGet(Name = nameof(GetReplies))]
        public async Task<ActionResult<ICollection<ReplyContentDto>>> GetReplies([FromRoute] uint articleId)
        {
            var replyList = await _replyService.GetAllAsync(articleId);

            if (replyList is null)
                return NotFound();

            // 返回评论Dto结果
            return Ok(
                _mapper.Map<ICollection<ReplyContentDto>>(replyList)
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
            // 获取回复
            var reply = await _replyService.GetAsync(articleId, replyId);

            // 判断是否找到回复
            if (reply is null)
            {
                return NotFound();
            }

            // 更新回复点赞
            _replyService.Like(reply);

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
            var article = await _articleService.GetAsync(articleId);

            // 判断是否找到文章
            if (article == null)
            {
                _logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            // Dto映射为实体
            var reply = _mapper.Map<Reply>(inputReply);

            // 回复文章
            var result = _replyService.Add(article, reply);

            if (result == null)
            {
                return Accepted();
            }

            // 返回结果
            var returnDto = _mapper.Map<ReplyContentDto>(result);
            return Created($"v1/articles/{articleId}/replies/{returnDto.ReplyId}", returnDto);
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
            var reply = await _replyService.GetAsync(articleId, replyId);

            // 判断是否找到回复
            if (reply is null)
                return NotFound();

            _replyService.Delete(reply);

            return NoContent();

        }

    }
}
