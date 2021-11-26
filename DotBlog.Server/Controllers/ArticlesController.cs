using AutoMapper;
using DotBlog.Server.Dto;
using DotBlog.Server.Entities;
using DotBlog.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using DotBlog.Shared;
using DotBlog.Shared.Dto;

namespace DotBlog.Server.Controllers
{
    [Route(Startup.ApiVersion + "/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        // 通过DI注入的只读服务

        /// <summary>
        /// 文章操作服务
        /// </summary>
        private readonly IArticlesService _articleService;

        /// <summary>
        /// 日志记录服务
        /// </summary>
        private readonly ILogger<ArticlesController> _logger;

        /// <summary>
        /// 对象映射服务
        /// </summary>
        private readonly IMapper _mapper;

        // 自定义字段

        /// <summary>
        /// 格式化JSON
        /// </summary>
        private readonly JsonSerializerOptions _printOptions = new() { WriteIndented = true };

        // 构造函数
        public ArticlesController(IArticlesService articleService, ILogger<ArticlesController> logger, IMapper mapper)
        {
            // 依赖注入
            _articleService = articleService ??
                              throw new ArgumentNullException(nameof(articleService));
            _logger = logger ??
                      throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="limit">限制数</param>
        /// <returns>HTTP 200</returns>
        [HttpGet]
        public async Task<ActionResult<ICollection<ArticleListDto>>> GetArticleList([FromQuery] int? limit)
        {
            _logger.LogInformation($"Match method {nameof(GetArticleList)}.");
            // 获取文章列表
            var articlesList = await _articleService.GetAllAsync(limit);

            // 判空
            if (articlesList.Count == 0)
            {
                _logger.LogInformation("No articles were found, return a empty list.");
            }

            _logger.LogDebug($"Find articles: {JsonSerializer.Serialize(articlesList, _printOptions)}");

            // 返回Dto结果
            return Ok(
                _mapper.Map<ICollection<ArticleListDto>>(articlesList)
            );
        }

        /// <summary>
        /// 获取文章
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 200/ HTTP 404 / HTTP 400</returns>
        [HttpGet("{articleId}", Name = nameof(GetArticle))]
        public async Task<ActionResult<ArticleContentDto>> GetArticle([FromRoute] uint articleId)
        {
            _logger.LogInformation($"Match method {nameof(GetArticle)}.");

            // 获取文章
            var article = await _articleService.GetAsync(articleId);

            // 判空
            if (article == null)
            {
                _logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            _logger.LogDebug($"Find article: {JsonSerializer.Serialize(article, _printOptions)}");

            // 返回Dto结果
            return Ok(
                _mapper.Map<ArticleContentDto>(article)
            );
        }


        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="inputArticle">文章实例</param>
        /// <returns>HTTP 200 / HTTP 404 / HTTP 400</returns>
        //[Authorize]
        [HttpPut("{articleId}")]
        public async Task<ActionResult<ArticleContentDto>> UpdateArticle([FromRoute] uint articleId, [FromBody] ArticleUpdateDto inputArticle)
        {
            _logger.LogInformation($"Match method {nameof(UpdateArticle)}.");

            // 获取旧文章
            var articleOld = await _articleService.GetAsync(articleId);

            if (articleOld == null)
            {
                _logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            // 更新文章
            _mapper.Map(inputArticle, articleOld);

            // 保存更改
            if (!await _articleService.SaveChangesAsync())
            {
                _logger.LogError("Cannot save changes, UnKnow error.");
            }

            // 返回Dto结果
            return Ok(
                _mapper.Map<ArticleContentDto>(articleOld)
            );
        }

        /// <summary>
        /// 更新文章点赞
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 204 / HTTP 404</returns>
        [HttpPut("{articleId}/Like")]
        public async Task<IActionResult> UpdateArticleLike([FromRoute] uint articleId)
        {
            _logger.LogInformation($"Match method {nameof(UpdateArticleLike)}.");

            // 获取文章
            var article = await _articleService.GetAsync(articleId);

            // 判断是否找到文章
            // ReSharper disable once InvertIf
            if (article == null)
            {
                _logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            // 更新点赞
            _articleService.Like(article);

            // ReSharper disable once InvertIf
            if (!await _articleService.SaveChangesAsync())
            {
                _logger.LogError("Cannot save changes, UnKnow error.");
            }

            // 返回结果
            return NoContent();
        }

        /// <summary>
        /// 更新文章阅读
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 204 / HTTP 404</returns>
        [HttpPut("{articleId}/Read")]
        public async Task<IActionResult> UpdateArticleRead([FromRoute] uint articleId)
        {
            _logger.LogInformation($"Match method {nameof(UpdateArticleRead)}.");

            // 获取文章
            var article = await _articleService.GetAsync(articleId);

            // 判断是否找到文章
            // ReSharper disable once InvertIf
            if (article == null)
            {
                _logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            // 更新文章阅读数
            _articleService.Read(article);

            // ReSharper disable once InvertIf
            if (!await _articleService.SaveChangesAsync())
            {
                _logger.LogError("Cannot save changes, UnKnow error.");
            }

            // 返回结果
            return NoContent();
        }

        /// <summary>
        /// 新建文章
        /// </summary>
        /// <param name="inputArticle">文章实例</param>
        /// <returns>HTTP 201 / HTTP 202 / HTTP 400</returns>
        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<ArticleContentDto>> CreateArticle([FromBody] ArticleAddDto inputArticle)
        {
            _logger.LogInformation($"Match method {nameof(CreateArticle)}.");

            // Dto映射为实体
            var article = _mapper.Map<Article>(inputArticle);

            // 新建文章
            var returnArticle = _articleService.PostArticle(article);

            if (returnArticle == null)
            {
                return Accepted();
            }

            if (!await _articleService.SaveChangesAsync())
            {
                _logger.LogError("Cannot save changes, UnKnow error.");
            }

            // 返回Dto结果
            var returnArticleDto = _mapper.Map<ArticleContentDto>(returnArticle);
            return CreatedAtRoute(nameof(GetArticle), new { articleId = returnArticleDto.ArticleId }, returnArticleDto);
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 204 / HTTP 404 / HTTP 400</returns>
        //[Authorize]
        [HttpDelete("{articleId}")]
        public async Task<IActionResult> DeleteArticle([FromRoute] uint articleId)
        {
            _logger.LogInformation($"Match method {nameof(DeleteArticle)}.");



            // 获取文章
            var article = await _articleService.GetAsync(articleId);

            // 判断是否找到文章
            // ReSharper disable once InvertIf
            if (article == null)
            {
                _logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            // 删除文章
            _articleService.DeleteArticle(article);

            if (!await _articleService.SaveChangesAsync())
            {
                _logger.LogError("Cannot save changes, UnKnow error.");
            }

            // 返回结果
            return NoContent();
        }
    }
}
