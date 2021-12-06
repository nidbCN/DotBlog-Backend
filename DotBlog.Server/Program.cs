using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using DotBlog.Server.Data;
using DotBlog.Server.Services;
using DotBlog.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://blog.gaein.cn",
                                "http://localhost:8080");
            builder.AllowAnyMethod();
        });
});

// 添加数据库上下文
builder.Services.AddDbContext<BlogDbContext>(
    options =>
    {
        var connStr = builder.Configuration.GetConnectionString("mysql");

        // options.UseNpgsql(connStr);
        options.UseMySql(connStr, new MySqlServerVersion("10.5"));
    }
);

// 添加 AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// 添加文章、回复服务
builder.Services.AddScoped<IArticlesService, ArticlesService>();
builder.Services.AddScoped<IRepliesService, RepliesService>();
builder.Services.AddScoped<IBlogsRepository, BlogsRepository>();

builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // 开发环境
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.MapControllers();

app.Run();
