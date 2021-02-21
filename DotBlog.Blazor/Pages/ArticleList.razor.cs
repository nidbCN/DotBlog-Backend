using DotBlog.Blazor.Services;
using DotBlog.Shared.Dto;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotBlog.Blazor.Pages
{
    public partial class ArticleList
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IArticleService ArticleService { get; set; }

        public IEnumerable<ArticleListDto> ArticleListGet { get; set; }

            /// <summary>
        /// 跳转到文章
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="articleAlias">文章别名</param>
        public void NavToArticle(uint articleId, string articleAlias)
        {
            NavigationManager.NavigateTo($"articles/{articleId}/{articleAlias}");
        }

        protected override async Task OnInitializedAsync()
        {
            ArticleListGet = await ArticleService.GetArticleListAsync();
            await base.OnInitializedAsync();
        }
    }
}
