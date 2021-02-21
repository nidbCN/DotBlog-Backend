using AntDesign.Pro.Layout;
using DotBlog.Blazor.Models;
using DotBlog.Blazor.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DotBlog.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            var service = builder.Services;
            builder.RootComponents.Add<App>("#app");

            // HTTP 相关服务注入
            var apiUrl = new Uri(
                builder.Configuration.GetSection("AppConfig:ApiUrl").Value
                ?? builder.HostEnvironment.BaseAddress
            );

            service.AddHttpClient<IArticleService, ArticleService>(opt=>
                opt.BaseAddress = apiUrl
            );
            service.AddHttpClient<IReplyService, ReplyService>(opt =>
                opt.BaseAddress = apiUrl
            );

            // AntDesign注入
            service.AddAntDesign();

            // 配置文件注入
            service.Configure<AppConfig>(builder.Configuration.GetSection("AppConfig"));
            service.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));

            // 服务注入
            service.AddScoped<IProjectService, ProjectService>();

            await builder.Build().RunAsync();
        }
    }
}
