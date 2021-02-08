using System;
using System.Threading.Tasks;
using AngleSharp.Dom;
using DotBlog.Server.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DotBlog.Server.Services;

namespace DotBlog.Server.Controllers
{
    [Route(Startup.ApiVersion + "Articles/{articleId}/[controller]")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        private IArticleService ArticleService { get; }
        private IReplyService ReplyService { get; }
        private ILogger<RepliesController> Logger { get; }
        private IActionResult ResetContent() => StatusCode(205);
        private IActionResult InternalServerError() => StatusCode(500);
        // 端点模板起始部分
        public RepliesController(IArticleService articleService, IReplyService replyService, ILogger<RepliesController> logger)
        {
            ArticleService = articleService;
            ReplyService = replyService;
            Logger = logger;
        }

        /// <summary>
        /// 获取回复
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 200</returns>
        [HttpGet]
        public async Task<IActionResult> GetReplies([FromRoute] Guid articleId)
        {
            // 判空
            if (articleId == Guid.Empty)
            {
                return BadRequest();
            }
            // 获取文章
            var articleItem = await ArticleService.GetArticleAsync(articleId);
            // 判空
            if (articleItem == null)
            {
                return NotFound();
            }
            // 返回评论列表
            return Ok(ReplyService.GetReplies(articleItem));
        }

        /// <summary>
        /// 更新回复点赞
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="replyId">回复ID</param>
        /// <returns>HTTP 200 / HTTP 204 / HTTP 400</returns>
        [HttpPatch("{replyId}/Like")]
        public IActionResult PatchReplyLike([FromRoute] Guid articleId, [FromRoute] Guid replyId)
        {
            if (articleId == Guid.Empty || replyId == Guid.Empty)
            {
                return BadRequest();
            }

            var articleItem = ArticleService.GetArticle(articleId);
            if (articleItem == null)
            {
                return NotFound();
            }
            var replyItem = ReplyService.GetReply(articleItem, replyId);
            if (replyItem == null)
            {
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
        /// <param name="replyItem">回复</param>
        /// <returns>HTTP 201 / HTTP 202 / HTTP 400</returns>
        [HttpPost]
        public IActionResult PostReply([FromRoute] Guid articleId, [FromBody] Reply replyItem)
        {
            if (articleId == Guid.Empty)
            {
                return BadRequest();
            }

            if (replyItem == null)
            {
                return BadRequest();
            }

            var articleItem = ArticleService.GetArticle(articleId);
            if (articleItem == null)
            {
                return NotFound();
            }

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
        public IActionResult DeleteReply([FromRoute] Guid articleId, [FromRoute] Guid replyId)
        {
            if (articleId == Guid.Empty || replyId == Guid.Empty)
            {
                return BadRequest();
            }

            var articleItem = ArticleService.GetArticle(articleId);
            if (articleItem == null)
            {
                return NotFound();
            }

            var replyItem = ReplyService.GetReply(articleItem, replyId);

            if (replyItem == null)
            {
                return NotFound();
            }

            return ReplyService.DeleteReply(articleItem, replyItem)
                ? ResetContent()
                : InternalServerError();

        }

    }
}
