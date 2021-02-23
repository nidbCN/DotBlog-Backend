using DotBlog.Shared.Dto;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace DotBlog.Blazor.Components
{
    public partial class ReplyListComponent
    {
        private string CardTitle => $"{Replies.Count}条回复：";

        [Parameter] public IList<ReplyContentDto> Replies { get; set; }

        private void LikeReplyAsync(uint replyId)
        {

        }
    }
}
