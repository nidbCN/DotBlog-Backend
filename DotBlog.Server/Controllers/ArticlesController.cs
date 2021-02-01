using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DotBlog.Server.Models;
using DotBlog.Server.Services;

namespace DotBlog.Server.Controllers
{
    [Route(Startup.ApiVersion + "/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private IArticleService ArticleService { get; }
        private ILogger<ArticlesController> Logger { get; }

        public ArticlesController(IArticleService articleService, ILogger<ArticlesController> logger)
        {
            ArticleService = articleService;
            Logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetArticles([FromQuery] int? limit)
        {
            Logger.LogInformation($"Match method {nameof(GetArticles)}.");
            var result = Ok(null);
            var articleList = await ArticleService.GetArticles(limit);

            if (articleList != null)
            {
                var articleRetList = ReturnArticles.Convert(articleList);
                result.Value = articleRetList;
            }

            return result;
        }
    }
}
