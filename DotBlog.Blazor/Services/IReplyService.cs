using DotBlog.Shared.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DotBlog.Blazor.Services
{
    public interface IReplyService
    {
        Task<IEnumerable<ReplyContentDto>> GetReplyListAsync(uint articleId);
    }
}
