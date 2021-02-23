using DotBlog.Blazor.Dto;
using DotBlog.Blazor.Services;
using DotBlog.Shared.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotBlog.Blazor.Components
{
    public partial class ReplyInputComponent
    {
        [Inject]
        public IReplyService ReplyService { get; set; }

        [Parameter] public IList<ReplyContentDto> Replies { get; set; }
        [Parameter] public string ArticleId { get; set; }

        private ReplyAddDto _newReply = new ();

        private async Task OnFinish(EditContext editContext)
        {
            var result = await ReplyService.AddReplyAsync(uint.Parse(ArticleId), _newReply);
            if (result == null)
            {
                // ERR
            }
            else
            {
                Replies.Add(result);
            }
        }

        private void OnFinishFailed(EditContext editContext)
        {
            // ERR
        }
    }
}
