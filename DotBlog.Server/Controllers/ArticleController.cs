using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DotBlog.Server.Entities;
using DotBlog.Server.Models;
using DotBlog.Server.Services;

namespace DotBlog.Server.Controllers
{
    [Route(Startup.ApiVersion + "/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private IArticleService ArticleService { get; }
        private ILogger Logger { get; }
        public ArticleController(IArticleService articleService, ILogger<ArticleController> logger)
        {
            ArticleService = articleService;
            Logger = logger;
        }

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

        [HttpPut("{articleId}")]
        public async Task<IActionResult> PutArticle([FromRoute] string articleId, [FromBody] Article articleItem)
        {
            Logger.LogInformation($"Match method {nameof(PutArticle)}.");
            var articleGuid = Guid.Parse(articleId);
            var result = await ArticleService.PutArticle(articleGuid, articleItem);
            return result == null ? Ok() : NotFound();
        }

        [HttpPatch("{articleId}/Like")]
        public async Task<IActionResult> PatchArticleLike([FromRoute] string articleId)
        {
            Logger.LogInformation($"Match method {nameof(PatchArticleLike)}.");
            var articleGuid = Guid.Parse(articleId);
            var result = await ArticleService.PatchArticleLike(articleGuid);
            return result ? NoContent() : NotFound();
        }

        [HttpPatch("{articleId}/Read")]
        public async Task<IActionResult> PatchArticleRead([FromRoute] string articleId)
        {
            Logger.LogInformation($"Match method {nameof(PatchArticleRead)}.");
            var articleGuid = Guid.Parse(articleId);
            var result = await ArticleService.PatchArticleRead(articleGuid);
            return result ? NoContent() : NotFound();
        }

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

        [HttpDelete("{articleId}")]
        public async Task<IActionResult> DeleteArticle([FromRoute] string articleId)
        {
            Logger.LogInformation($"Match method {nameof(DeleteArticle)}.");
            var articleGuid = Guid.Parse(articleId);
            var result = await ArticleService.DeleteArticle(articleGuid);
            return result ? NoContent() : NotFound();
        }
    }
}
