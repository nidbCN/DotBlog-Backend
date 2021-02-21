using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using DotBlog.Server.Data;
using DotBlog.Server.Services;

namespace DotBlog.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// API �汾
        /// </summary>
        public const string ApiVersion = "v1";

        public IConfiguration Configuration { get; }

        // ����ע������
        public void ConfigureServices(IServiceCollection services)
        {
            // ������ݿ�������
            services.AddDbContext<DotBlogDbContext>(
                options => options.UseSqlite(Configuration.GetConnectionString("SqLite"))
            );

            // ��� AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            // ������¡��ظ�����
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IReplyService, ReplyService>();

            services.AddLogging();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApiVersion, new OpenApiInfo { Title = "DotBlog Server  - Powered by .NET 5.0", Version = ApiVersion });
            });

            services.AddCors(options =>
                options.AddPolicy("Open", builder => 
                    builder.AllowAnyHeader()
                        .AllowAnyOrigin()
                    )
                );
        }

        // HTTP �ܵ��м������
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // ��������
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(opt => 
                    opt.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", $"DotBlog Server {ApiVersion}")
                );
            }

            // HTTPS �ض���
            app.UseHttpsRedirection();

            app.UseRouting();

            // ����
            app.UseCors("Open");
            //// �����֤
            //app.UseAuthentication();
            //// ��Ȩ
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
