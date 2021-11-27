using AutoMapper;
using DotBlog.Server.Dto;
using DotBlog.Server.Dto.QueryModel;
using DotBlog.Server.Entities;
using DotBlog.Server.Services;
using DotBlog.Shared.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotBlog.Server.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        #region 只读字段
        /// <summary>
        /// 文章操作服务
        /// </summary>
        private readonly IArticlesService _articlesService;

        /// <summary>
        /// 日志记录服务
        /// </summary>
        private readonly ILogger<ArticlesController> _logger;

        /// <summary>
        /// 对象映射服务
        /// </summary>
        private readonly IMapper _mapper;
        #endregion

        #region 构造函数
        public ArticlesController(IArticlesService articlesService, ILogger<ArticlesController> logger, IMapper mapper)
        {
            // 依赖注入
            _articlesService = articlesService;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region WebApi

        #region 获取相关
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="count">数量</param>
        /// <returns>文章集合</returns>
        [HttpGet]
        public async Task<ActionResult<IList<ArticleListDto>>> GetArticleList([FromQuery]ArticleGetDtoParameters? query)
        {
            var articlesList = query is null
                ? await _articlesService.GetAllAsync()
                : await _articlesService.GetAllAsync(query);

            // 返回Dto结果
            return Ok(
                _mapper.Map<ICollection<ArticleListDto>>(articlesList)
            );
        }

        /// <summary>
        /// 获取文章
        /// </summary>
        /// <param name="articleId">文章id</param>
        /// <returns>文章实例</returns>
        [HttpGet("{articleId}", Name = nameof(GetArticle))]
        public async Task<ActionResult<ArticleContentDto>> GetArticle(uint articleId)
        {
            // 获取文章
            var article = await _articlesService.GetAsync(articleId);

            // 判空
            if (article is null)
            {
                return NotFound();
            }

            // 返回Dto结果
            return Ok(
                _mapper.Map<ArticleContentDto>(article)
            );
        }

        #endregion

        #region 更新相关

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="articleId">文章id</param>
        /// <param name="patchDocument">Patch Document</param>
        /// <returns></returns>
        [HttpPatch("{articleId}")]
        public async Task<IActionResult> UpdateArticle(
            uint articleId, 
            JsonPatchDocument<ArticleUpdateDto> patchDocument)
        {
            var article = await _articlesService.GetAsync(articleId);

            if (article is null)
                return NotFound();

            var dtoToPatch = _mapper.Map<ArticleUpdateDto>(article);

            // TODO: 需要处理验证错误
            patchDocument.ApplyTo(dtoToPatch);

            _mapper.Map(dtoToPatch, article);

            await _articlesService.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// 更新文章点赞
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns></returns>
        [HttpPut("{articleId}/Like")]
        public async Task<IActionResult> UpdateArticleLike([FromRoute] uint articleId)
        {
            // 获取文章
            var article = await _articlesService.GetAsync(articleId);

            // 判断是否找到文章
            // ReSharper disable once InvertIf
            if (article == null)
            {
                _logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            // 更新点赞
            _articlesService.Like(article);

            await _articlesService.SaveAsync();

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
            var article = await _articlesService.GetAsync(articleId);

            // 判断是否找到文章
            // ReSharper disable once InvertIf
            if (article == null)
            {
                _logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            // 更新文章阅读数
            _articlesService.Read(article);

            // ReSharper disable once InvertIf
            await _articlesService.SaveAsync();

            // 返回结果
            return NoContent();
        }

        #endregion

        #region 新建相关

        /// <summary>
        /// 新建文章
        /// </summary>
        /// <param name="articleDto">文章实例</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ArticleContentDto>> CreateArticle([FromBody] ArticleAddDto articleDto)
        {
            var article = _mapper.Map<Article>(articleDto);

            // 新建文章
            var addedArticle = _articlesService.Add(article);

            if (addedArticle is null)
            {
                return Accepted();
            }

            await _articlesService.SaveAsync();

            // 返回Dto结果
            var returnedArticleDto = _mapper.Map<ArticleContentDto>(addedArticle);
            return CreatedAtRoute(nameof(GetArticle), new { articleId = returnedArticleDto.ArticleId }, returnedArticleDto);
        }

        #endregion

        #region 删除相关

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>HTTP 204 / HTTP 404 / HTTP 400</returns>
        [HttpDelete("{articleId}")]
        public async Task<IActionResult> DeleteArticle([FromRoute] uint articleId)
        {
            // 获取文章
            var article = await _articlesService.GetAsync(articleId);

            // 判断是否找到文章
            // ReSharper disable once InvertIf
            if (article == null)
            {
                _logger.LogInformation("No article was found, return a NotFound.");
                return NotFound();
            }

            // 删除文章
            _articlesService.Remove(article);

            await _articlesService.SaveAsync();

            // 返回结果
            return NoContent();
        }
        #endregion

        #endregion
    }
}
