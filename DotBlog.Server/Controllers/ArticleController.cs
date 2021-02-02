using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DotBlog.Server.Entities;
using DotBlog.Server.Models;
using DotBlog.Server.Services;
using Microsoft.AspNetCore.Authorization;

namespace DotBlog.Server.Controllers
{
    // TODO(mail@gaein.cn): 异常处理
    [Route(Startup.ApiVersion + "/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private IArticleService ArticleService { get; }
        private IReplyService ReplyService { get; }
        private ILogger<ArticleController> Logger { get; }
        public ArticleController(IArticleService articleService, IReplyService replyService, ILogger<ArticleController> logger)
        {
            ArticleService = articleService;
            ReplyService = replyService;
            Logger = logger;
        }

        /// <summary>
        /// 获取文章
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 200</returns>
        [HttpGet("{articleId}")]
        public async Task<IActionResult> GetArticle([FromRoute] string articleId)
        {
            Logger.LogInformation($"Match method {nameof(GetArticle)}.");

            if (articleId == null)
            {
                return BadRequest();
            }

            var articleGuid = Guid.Parse(articleId);
            var articleItem = await ArticleService.GetArticle(articleGuid);

            if (articleItem == null)
            {
                Logger.LogWarning("No article was found.");
                return NotFound();
            }

            Logger.LogDebug($"Find article: {JsonSerializer.Serialize(articleItem)}");

            var articleRetItem = ReturnArticle.Convert(articleItem);
            var returnItem = new ReturnUniversally(articleRetItem);
            return Ok(returnItem);
        }

        /// <summary>
        /// 获取回复
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 200</returns>
        [HttpGet("{articleId}/replies/")]
        public async Task<IActionResult> GetReplies([FromRoute] string articleId)
        {
            var articleGuid = Guid.Parse(articleId);
            var repliesItem = await ReplyService.GetReplies(articleGuid);

            var ret = Ok(null);

            if (repliesItem != null)
            {
                var returnItem = new ReturnUniversally(repliesItem);
                ret = Ok(returnItem);
            }

            return ret;
        }


        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="articleItem">文章实例</param>
        /// <returns></returns>
        //[Authorize]
        [HttpPut("{articleId}")]
        public async Task<IActionResult> PutArticle([FromRoute] string articleId, [FromBody] Article articleItem)
        {
            Logger.LogInformation($"Match method {nameof(PutArticle)}.");
            var articleGuid = Guid.Parse(articleId);

            var result = await ArticleService.PutArticle(articleGuid, articleItem);
            return result == null ? Ok() : NotFound();
        }

        /// <summary>
        /// 更新文章点赞
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 204 / HTTP 404</returns>
        [HttpPatch("{articleId}/Like")]
        public async Task<IActionResult> PatchArticleLike([FromRoute] string articleId)
        {
            Logger.LogInformation($"Match method {nameof(PatchArticleLike)}.");
            var articleGuid = Guid.Parse(articleId);
            var result = await ArticleService.PatchArticleLike(articleGuid);
            return result ? NoContent() : NotFound();
        }

        /// <summary>
        /// 更新文章阅读
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 204 / HTTP 404</returns>
        [HttpPatch("{articleId}/Read")]
        public async Task<IActionResult> PatchArticleRead([FromRoute] string articleId)
        {
            Logger.LogInformation($"Match method {nameof(PatchArticleRead)}.");
            var articleGuid = Guid.Parse(articleId);
            var result = await ArticleService.PatchArticleRead(articleGuid);
            return result ? NoContent() : NotFound();
        }

        /// <summary>
        /// 更新回复点赞
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="replyId">回复ID</param>
        /// <returns>HTTP 200 / HTTP 204 / HTTP 400</returns>
        [HttpPatch("{articleId}/reply/{replyId}/Like")]
        public async Task<IActionResult> PatchReplyLike([FromRoute] string articleId, [FromRoute] string replyId)
        {
            try
            {
                var articleGuid = Guid.Parse(articleId);
                var replyGuid = Guid.Parse(replyId);

                var result = await ReplyService.PatchReplyLike(articleGuid, replyGuid);

                return result ? NoContent() : NotFound();
            }
            catch (Exception e)
            {
                Logger.LogError($"Error in {nameof(PatchArticleLike)}:{e.Message}");
                return BadRequest();
            }
        }

        /// <summary>
        /// 新建文章
        /// </summary>
        /// <param name="articleItem">文章实例</param>
        /// <returns>HTTP 201 / HTTP 202 / HTTP 400</returns>
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> PostArticle([FromBody] Article articleItem)
        {
            Logger.LogInformation($"Match method {nameof(PutArticle)}.");

            if (articleItem == null)
            {
                return BadRequest();
            }

            var result = await ArticleService.PostArticle(articleItem);
            return result == null ? Accepted() : Created(result.ResourceUri, result);
        }

        /// <summary>
        /// 新建回复
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="replyItem">回复ID</param>
        /// <returns>HTTP 201 / HTTP 202 / HTTP 400</returns>
        [HttpPost("{articleId}/reply")]
        public async Task<IActionResult> PostReply([FromRoute] string articleId, [FromBody] Reply replyItem)
        {
            if (replyItem == null)
            {
                return BadRequest();
            }

            var articleGuid = Guid.Parse(articleId);
            var result = await ReplyService.PostReply(articleGuid, replyItem);
            return result == null
                ? Accepted()
                : Created(result.ResourceUri, result);
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 204 / HTPP 404</returns>
        //[Authorize]
        [HttpDelete("{articleId}")]
        public async Task<IActionResult> DeleteArticle([FromRoute] string articleId)
        {
            Logger.LogInformation($"Match method {nameof(DeleteArticle)}.");
            var articleGuid = Guid.Parse(articleId);
            var result = await ArticleService.DeleteArticle(articleGuid);
            return result ? NoContent() : NotFound();
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="replyId">评论ID</param>
        /// <returns></returns>
        //[Authorize]
        [HttpDelete("{articleId}/reply/{replyID}")]
        public async Task<IActionResult> DeleteReply([FromRoute] Guid articleId, [FromRoute] Guid replyId)
        {
            if (articleId == Guid.Empty || replyId == Guid.Empty)
            {
                return BadRequest();
            }

            var result = await ReplyService.DeleteReply(articleId, replyId);
            return result ? NoContent() : NotFound();

        }
    }
}
