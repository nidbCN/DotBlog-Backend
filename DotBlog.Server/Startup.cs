using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using DotBlog.Server.Data;
using DotBlog.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace DotBlog.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// API 版本
        /// </summary>
        public const string ApiVersion = "v1";

        public IConfiguration Configuration { get; }

        // 服务注入配置
        public void ConfigureServices(IServiceCollection services)
        {
            // 添加数据库上下文
            services.AddDbContext<DotBlogDbContext>(
                options => options.UseSqlite(Configuration.GetConnectionString("SqLite"))
            );

            // 添加 AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            // 添加文章、回复服务
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IReplyService, ReplyService>();

            services.AddLogging();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApiVersion, new OpenApiInfo { Title = "DotBlog Server  - Powered by .NET 5.0", Version = ApiVersion });
            });
        }

        // HTTP 管道中间件配置
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // 开发环境
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(opt => 
                    opt.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", $"DotBlog Server {ApiVersion}")
                );
            }

            // HTTPS 重定向
            app.UseHttpsRedirection();

            app.UseRouting();

            //// 身份验证
            //app.UseAuthentication();
            //// 授权
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
