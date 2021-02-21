using DotBlog.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotBlog.Blazor.Services
{
    public class ReplyService : IReplyService
    {
        public HttpClient HttpClient { get; }

        /// <summary>
        /// JSON 名称不区分大小写
        /// </summary>
        private readonly JsonSerializerOptions _jsonOptionPropertyNameCaseInsensitive = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        private const string BaseRoute = "v1/articles/";

        public ReplyService(HttpClient httpClient)
        {
            HttpClient = httpClient
                         ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<ReplyContentDto>> GetReplyListAsync(uint articleId) =>
            await JsonSerializer.DeserializeAsync<IEnumerable<ReplyContentDto>>(
                await HttpClient.GetStreamAsync(BaseRoute + articleId + "/replies"), _jsonOptionPropertyNameCaseInsensitive
            );
    }
}
