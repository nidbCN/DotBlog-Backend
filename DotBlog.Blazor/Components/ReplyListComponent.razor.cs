using DotBlog.Shared.Dto;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace DotBlog.Blazor.Components
{
    public partial class ReplyListComponent
    {
        private string CardTitle => $"{Replies.Count()}条回复：";

        [Parameter] public IEnumerable<ReplyContentDto> Replies { get; set; }
    }
}
