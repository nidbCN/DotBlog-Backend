using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotBlog.Server.Services;
using Microsoft.Extensions.Logging;

namespace DotBlog.Server.Controllers
{
    [Route(Startup.ApiVersion + "article/{**}/[controller]")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        private IArticleService ArticleService { get; }
        private IReplyService ReplyService { get; }
        private ILogger<RepliesController> Logger { get; }
    }
}
