using DotBlog.Blazor.Dto;
using DotBlog.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        private const string BaseRoute = "articles/";

        public ReplyService(HttpClient httpClient)
        {
            HttpClient = httpClient
                         ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IList<ReplyContentDto>> GetReplyListAsync(uint articleId) =>
            await JsonSerializer.DeserializeAsync<IList<ReplyContentDto>>(
                await HttpClient.GetStreamAsync(BaseRoute + articleId + "/replies"), _jsonOptionPropertyNameCaseInsensitive
            );

        public async Task<ReplyContentDto> AddReplyAsync(uint articleId, ReplyAddDto reply)
        {
            var replyJson = new StringContent(
                JsonSerializer.Serialize(reply),
                Encoding.UTF8,
                "application/json"
            );

            var response = await HttpClient.PostAsync(
                BaseRoute + articleId + "/replies", replyJson
            );

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<ReplyContentDto>(
                    await response.Content.ReadAsStreamAsync()
                );
            }

            return null;
        }

        public async Task LikeReplyAsync(uint articleId, uint replyId)
        {
            HttpContent content = new StringContent("");
            await HttpClient.PostAsync(BaseRoute + articleId + "/Replies" + replyId + "/Like", content);
        }
    }
}
