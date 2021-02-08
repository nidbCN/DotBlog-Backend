using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DotBlog.Server.Models;
using DotBlog.Server.Services;
using System.Text.Json;
using System;
using DotBlog.Server.Entities;

namespace DotBlog.Server.Controllers
{
    [Route(Startup.ApiVersion + "/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private IArticleService ArticleService { get; }
        private ILogger<ArticlesController> Logger { get; }

        private IActionResult ResetContent() => StatusCode(205);
        private IActionResult InternalServerError() => StatusCode(500);

        public ArticlesController(IArticleService articleService, ILogger<ArticlesController> logger)
        {
            ArticleService = articleService;
            Logger = logger;
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="limit">限制数</param>
        /// <returns>HTTP 200</returns>
        [HttpGet]
        public async Task<IActionResult> GetArticles([FromQuery] int? limit)
        {
            Logger.LogInformation($"Match method {nameof(GetArticles)}.");
            // 获取文章列表
            var articleList = await ArticleService.GetArticlesAsync(limit);
            // 判空
            if (articleList == null)
            {
                Logger.LogWarning("No articles were found, can not return list.");
                return Ok(
                    new ReturnUniversally(null, 0, 404, "No articles found.")
                );
            }

            Logger.LogDebug($"Find article: {JsonSerializer.Serialize(articleList)}");

            // 转换通用结果
            var articleRetList = ReturnArticles.Convert(articleList);
            return Ok(
                new ReturnUniversally(articleRetList, (uint)articleRetList.Count)
            );
        }

        /// <summary>
        /// 获取文章
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 200/ HTTP 404 / HTTP 400</returns>
        [HttpGet("{articleId}")]
        public async Task<IActionResult> GetArticle([FromRoute] Guid articleId)
        {
            Logger.LogInformation($"Match method {nameof(GetArticle)}.");

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
                Logger.LogWarning("No article was found, can not return item.");
                return NotFound();
            }

            Logger.LogDebug($"Find article: {JsonSerializer.Serialize(articleItem)}");

            // 转换通用结果
            var articleRetItem = ReturnArticle.Convert(articleItem);
            return Ok(
                new ReturnUniversally(articleRetItem, 1)
            );
        }


        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="articleItem">文章实例</param>
        /// <returns>HTTP 200 / HTTP 404 / HTTP 400 / HTTP 500?</returns>
        //[Authorize]
        [HttpPut("{articleId}")]
        public IActionResult PutArticle([FromRoute] Guid articleId, [FromBody] Article articleItem)
        {
            Logger.LogInformation($"Match method {nameof(PutArticle)}.");
            if (articleItem == null)
            {
                return BadRequest();
            }

            var articleOld = ArticleService.GetArticle(articleId);

            if (articleOld == null)
            {
                return NotFound();
            }

            var result = ArticleService.PutArticle(articleOld, articleItem);
            return result != null ? Ok() : StatusCode(500);
        }

        /// <summary>
        /// 更新文章点赞
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 205 / HTTP 404 / HTTP 500?</returns>
        [HttpPatch("{articleId}/Like")]
        public IActionResult PatchArticleLike([FromRoute] Guid articleId)
        {
            Logger.LogInformation($"Match method {nameof(PatchArticleLike)}.");

            // 判空
            if (articleId == Guid.Empty)
            {
                return BadRequest();
            }

            // 获取文章
            var article = ArticleService.GetArticle(articleId);

            // 判断是否找到文章
            if (article == null)
            {
                return NotFound();
            }
            return ArticleService.PatchArticleLike(article)
                ? ResetContent()
                : InternalServerError();
        }

        /// <summary>
        /// 更新文章阅读
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 205 / HTTP 404 / HTTP 500?</returns>
        [HttpPatch("{articleId}/Read")]
        public IActionResult PatchArticleRead([FromRoute] Guid articleId)
        {
            Logger.LogInformation($"Match method {nameof(PatchArticleRead)}.");

            // 判空
            if (articleId == Guid.Empty)
            {
                return BadRequest();
            }

            var article = ArticleService.GetArticle(articleId);

            if (article == null)
            {
                return BadRequest();
            }

            return ArticleService.PatchArticleRead(article)
                ? ResetContent()
                : InternalServerError();
        }

        /// <summary>
        /// 新建文章
        /// </summary>
        /// <param name="articleItem">文章实例</param>
        /// <returns>HTTP 201 / HTTP 202 / HTTP 400</returns>
        //[Authorize]
        [HttpPost]
        public IActionResult PostArticle([FromBody] Article articleItem)
        {
            Logger.LogInformation($"Match method {nameof(PutArticle)}.");

            if (articleItem == null)
            {
                return BadRequest();
            }

            var result = ArticleService.PostArticle(articleItem);
            return result != null
                ? Created(result.ResourceUri, result)
                : Accepted();
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 204 / HTTP 404 / HTTP 400 /HTTP 500?</returns>
        //[Authorize]
        [HttpDelete("{articleId}")]
        public IActionResult DeleteArticle([FromRoute] Guid articleId)
        {
            Logger.LogInformation($"Match method {nameof(DeleteArticle)}.");
            if (articleId == Guid.Empty)
            {
                return BadRequest();
            }
            var article = ArticleService.GetArticle(articleId);

            if (article == null)
            {
                return NotFound();
            }
            return ArticleService.DeleteArticle(article) 
                ? ResetContent()
                : InternalServerError();
        }
    }
}
