using DotBlog.Blazor.Services;
using DotBlog.Shared.Dto;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotBlog.Blazor.Pages
{
    public partial class ArticleContent
    {
        private string _articleId;

        [Parameter]
        public string ArticleId
        {
            get => _articleId;
            set => _articleId = value.Split('/', 2)[0];
        }

        [Inject] public IReplyService ReplyService { get; set; }
        [Inject] public IArticleService ArticleService { get; set; }

        public ArticleContentDto ArticleContentGet { get; set; }
        public IEnumerable<ReplyContentDto> ReplyListGet { get; set; } = new List<ReplyContentDto>();

        protected override async Task OnInitializedAsync()
        {
            ArticleContentGet = await ArticleService.GetArticleAsync(uint.Parse(ArticleId));
            ReplyListGet = await ReplyService.GetReplyListAsync(uint.Parse(ArticleId));
            await base.OnInitializedAsync();
        }
    }
}
