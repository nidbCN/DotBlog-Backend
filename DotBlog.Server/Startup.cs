using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using DotBlog.Server.Data;
using DotBlog.Server.Models;
using DotBlog.Server.Services;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace DotBlog.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var baseUrlConfig = Configuration["AppConfig:BaseUrl"];
            if (string.IsNullOrWhiteSpace(baseUrlConfig))
            {
                BaseUrl = string.Empty;
            }
            else if(!baseUrlConfig.StartsWith('/'))
            {
                BaseUrl = "/" + baseUrlConfig;
            }
            else
            {
                BaseUrl = baseUrlConfig;
            }
        }

        /// <summary>
        /// API 版本
        /// </summary>
        public const string ApiVersion = "v1";

        public IConfiguration Configuration { get; }

        public string BaseUrl { get; }

        // 服务注入配置
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppConfig>(
                Configuration.GetSection("AppConfig")
            );


            // 添加数据库上下文
            services.AddDbContext<DotBlogDbContext>(
                options =>
                {
                    var dbType = 
                        Configuration.GetSection("DataBase")?.Value ?? "SqLite";
                    var connStr = Configuration.GetConnectionString(dbType);

                    switch (dbType.ToLower())
                    {
                        case "postgresql":
                            options.UseNpgsql(connStr);
                            break;
                        // case "mysql":
                        //     options.UseMySQL(connStr);
                        //     break;
                        default:
                            options.UseSqlite(connStr);
                            break;
                    }
                }
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
                c.AddServer(new OpenApiServer{Url = BaseUrl});
                c.SwaggerDoc(ApiVersion, new OpenApiInfo { Title = "DotBlog Server  - Powered by .NET 5.0", Version = ApiVersion });
            });


            services.AddCors(options =>
                options.AddPolicy("Open", builder =>
                    builder.AllowAnyHeader()
                        .AllowAnyOrigin()
                    )
                );
        }

        // HTTP 管道中间件配置
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // 开发环境
                app.UseDeveloperExceptionPage();
            }

            // 开启Swagger页面
            app.UseSwagger(opt=>
                opt.RouteTemplate = "/docs/{documentName}/swagger.json"
            );

            app.UseSwaggerUI(opt =>
            {
                opt.RoutePrefix = "docs";
                opt.SwaggerEndpoint($"{ApiVersion}/swagger.json", $"DotBlog Server {ApiVersion}");
            });

            // HTTPS 重定向
            // app.UseHttpsRedirection();

            app.UseRouting();

            // 跨域
            app.UseCors("Open");
            //// 身份验证
            // app.UseAuthentication();
            //// 授权
            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
