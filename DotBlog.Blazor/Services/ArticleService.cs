using DotBlog.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotBlog.Blazor.Services
{
    public class ArticleService : IArticleService
    {
        public HttpClient HttpClient { get; }

        /// <summary>
        /// JSON 名称不区分大小写
        /// </summary>
        private readonly JsonSerializerOptions _jsonOptionPropertyNameCaseInsensitive = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        private const string BaseRoute = "articles/";

        public ArticleService(HttpClient httpClient)
        {
            HttpClient = httpClient
                         ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<ArticleListDto>> GetArticleListAsync() =>
            await JsonSerializer.DeserializeAsync<IEnumerable<ArticleListDto>>(
                await HttpClient.GetStreamAsync(BaseRoute), _jsonOptionPropertyNameCaseInsensitive
            );

        public async Task<ArticleContentDto> GetArticleAsync(uint articleId) =>
            await JsonSerializer.DeserializeAsync<ArticleContentDto>(
                await HttpClient.GetStreamAsync(BaseRoute + articleId), _jsonOptionPropertyNameCaseInsensitive
            );

        public async Task UpdateArticleLikeAsync(uint articleId)
        {
            HttpContent content = new StringContent("");
            await HttpClient.PostAsync(BaseRoute + articleId +"/Like", content);
        }

        public async Task UpdateArticleReadAsync(uint articleId)
        {
            HttpContent content = new StringContent("");
            await HttpClient.PostAsync(BaseRoute + articleId + "/Read", content);
        }
    }
}
