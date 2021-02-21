using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using AntDesign.Pro.Layout;
using DotBlog.Blazor.Models;

namespace DotBlog.Blazor.Services
{
    public class ProjectService : IProjectService
    {
        public HttpClient HttpClient { get; }
        public IOptions<AppConfig> Options { get; }

        public ProjectService(HttpClient httpClient, IOptions<AppConfig> options)
        {
            HttpClient = httpClient
                         ?? throw new ArgumentNullException(nameof(httpClient));
            Options = options
                      ?? throw new ArgumentNullException(nameof(options));
        }

        public IEnumerable<MenuDataItem> GetMenuData() =>
            Options.Value.MenuItems;
    }
}
