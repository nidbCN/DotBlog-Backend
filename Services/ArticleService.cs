using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace DotBlog.Blazor.Services
{
    public class ArticleService : IArticleService
    {
        public HttpClient HttpClient { get; }

        public ArticleService(HttpClient httpClient)
        {
            HttpClient = httpClient
                         ?? throw new ArgumentNullException(nameof(httpClient));
        }

        //public async Task<IEnumerable<ArticleListDto>> GetArticleListAsync()
        //{
        //    return await HttpClient.GetFromJsonAsync<IEnumerable<ArticleListDto>>("articles/");
        //}

        //public Task<ArticleContentDto> GetArticleAsync(uint articleId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
