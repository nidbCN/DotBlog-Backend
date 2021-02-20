using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
//using DotBlog.Shared.Dto;

namespace DotBlog.Blazor.Services
{
    public class ReplyService : IReplyService
    {
        public HttpClient HttpClient { get; }

        public ReplyService(HttpClient httpClient)
        {
            HttpClient = httpClient
                         ?? throw new ArgumentNullException(nameof(httpClient));
        }
        //public Task<IEnumerable<ReplyContentDto>> GetReplyListAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
