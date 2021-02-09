using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using AutoMapper;
using Masuit.Tools.Html;
using DotBlog.Server.Entities;
using DotBlog.Server.Services;
using DotBlog.Server.Models;

namespace DotBlog.Server.Controllers
{
    [Route(Startup.ApiVersion + "/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        // 通过DI注入的只读服务
        private IArticleService ArticleService { get; }
        private ILogger<ArticlesController> Logger { get; }
        private IMapper Mapper { get; }

        // 自定义返回类型
        private StatusCodeResult ResetContent() => StatusCode(205);
        private StatusCodeResult InternalServerError() => StatusCode(500);

        // 自定义字段
        private JsonSerializerOptions PrintOptions { get; }
            = new() { WriteIndented = true };

        // 构造函数
        public ArticlesController(IArticleService articleService, ILogger<ArticlesController> logger, IMapper mapper)
        {
            ArticleService = articleService;
            Logger = logger;
            Mapper = mapper;
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="limit">限制数</param>
        /// <returns>HTTP 200</returns>
        [HttpGet]
        public async Task<ActionResult<ICollection<ArticlesDto>>> GetArticles([FromQuery] int? limit)
        {
            Logger.LogInformation($"Match method {nameof(GetArticles)}.");
            // 获取文章列表
            var articleList = await ArticleService.GetArticlesAsync(limit);
            // 判空
            if (articleList == null)
            {
                Logger.LogInformation("No articles were found, return a empty list.");
                return Ok();
            }

            Logger.LogDebug($"Find articles: {JsonSerializer.Serialize(articleList, PrintOptions)}");

            // 返回Dto结果
            return Ok(
                Mapper.Map<ICollection<ArticlesDto>>(articleList)
            );
        }

        /// <summary>
        /// 获取文章
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 200/ HTTP 404 / HTTP 400</returns>
        [HttpGet("{articleId}")]
        public async Task<ActionResult<ArticleDto>> GetArticle([FromRoute] uint? articleId)
        {
            Logger.LogInformation($"Match method {nameof(GetArticle)}.");

            // 判空
            if (articleId == null)
            {
                Logger.LogInformation($"No {nameof(articleId)} input, return a BadRequest.");
                return BadRequest();
            }

            // 获取文章
            var articleItem = await ArticleService.GetArticleAsync((uint)articleId);

            // 判空
            if (articleItem == null)
            {
                Logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            Logger.LogDebug($"Find article: {JsonSerializer.Serialize(articleItem, PrintOptions)}");

            // 返回Dto结果
            return Ok(
                Mapper.Map<ArticleDto>(articleItem)
            );
        }


        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="articleItemDto">文章实例</param>
        /// <returns>HTTP 200 / HTTP 404 / HTTP 400 / HTTP 500?</returns>
        //[Authorize]
        [HttpPut("{articleId}")]
        public ActionResult<ArticleDto> PutArticle([FromRoute] uint? articleId, [FromBody] ArticleDto articleItemDto)
        {
            Logger.LogInformation($"Match method {nameof(PutArticle)}.");
            // 判空
            if (articleItemDto == null || articleId == null)
            {
                Logger.LogInformation($"No {nameof(articleItemDto)} or {nameof(articleId)} input, return a BadRequest.");
                return BadRequest();
            }

            // 安全检查
            articleItemDto.Category.HtmlSantinizerStandard();
            articleItemDto.Author.HtmlSantinizerStandard();
            articleItemDto.Title.HtmlSantinizerStandard();
            articleItemDto.Content.HtmlSantinizerStandard();
            articleItemDto.Description.HtmlSantinizerStandard();

            // Dto映射为实体
            var articleItem = Mapper.Map<Article>(articleItemDto);

            // 获取旧文章
            var articleOld = ArticleService.GetArticle((uint)articleId);

            if (articleOld == null)
            {
                Logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            var result = ArticleService.PutArticle(articleOld, articleItem);
            return result != null ? Ok(result) : InternalServerError();
        }

        /// <summary>
        /// 更新文章点赞
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 205 / HTTP 404 / HTTP 500?</returns>
        [HttpPatch("{articleId}/Like")]
        public IActionResult PatchArticleLike([FromRoute] uint? articleId)
        {
            Logger.LogInformation($"Match method {nameof(PatchArticleLike)}.");

            // 判空
            if (articleId == null)
            {
                Logger.LogInformation($"No {nameof(articleId)} input, return a BadRequest.");
                return BadRequest();
            }

            // 获取文章
            var articleItem = ArticleService.GetArticle((uint)articleId);

            // 判断是否找到文章
            // ReSharper disable once InvertIf
            if (articleItem == null)
            {
                Logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            return ArticleService.PatchArticleLike(articleItem)
                ? ResetContent()
                : InternalServerError();
        }

        /// <summary>
        /// 更新文章阅读
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 205 / HTTP 404 / HTTP 500?</returns>
        [HttpPatch("{articleId}/Read")]
        public IActionResult PatchArticleRead([FromRoute] uint? articleId)
        {
            Logger.LogInformation($"Match method {nameof(PatchArticleRead)}.");

            // 判空
            if (articleId == null)
            {
                Logger.LogInformation($"No {nameof(articleId)} input, return a BadRequest.");
                return BadRequest();
            }

            var article = ArticleService.GetArticle((uint)articleId);

            if (article == null)
            {
                Logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            return ArticleService.PatchArticleRead(article)
                ? ResetContent()
                : InternalServerError();
        }

        /// <summary>
        /// 新建文章
        /// </summary>
        /// <param name="articleItemDto">文章实例</param>
        /// <returns>HTTP 201 / HTTP 202 / HTTP 400</returns>
        //[Authorize]
        [HttpPost]
        public IActionResult PostArticle([FromBody] ArticleDto articleItemDto)
        {
            Logger.LogInformation($"Match method {nameof(PostArticle)}.");

            if (articleItemDto == null)
            {
                Logger.LogInformation($"No {nameof(articleItemDto)} input, return a BadRequest.");
                return BadRequest();
            }

            // 安全检查
            articleItemDto.Category.HtmlSantinizerStandard();
            articleItemDto.Author.HtmlSantinizerStandard();
            articleItemDto.Title.HtmlSantinizerStandard();
            articleItemDto.Content.HtmlSantinizerStandard();
            articleItemDto.Description.HtmlSantinizerStandard();

            var articleItem = Mapper.Map<Article>(articleItemDto);

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
        public IActionResult DeleteArticle([FromRoute] uint? articleId)
        {
            Logger.LogInformation($"Match method {nameof(DeleteArticle)}.");
            if (articleId == null)
            {
                Logger.LogInformation($"No {nameof(articleId)} input, return a BadRequest.");
                return BadRequest();
            }

            var article = ArticleService.GetArticle((uint)articleId);

            if (article == null)
            {
                Logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }
            return ArticleService.DeleteArticle(article)
                ? ResetContent()
                : InternalServerError();
        }
    }
}
