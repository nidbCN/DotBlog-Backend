using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using DotBlog.Server.Data;
using DotBlog.Server.Models;
using DotBlog.Server.Services;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration.GetSection(nameof(AppConfig));

builder.Services.Configure<AppConfig>(config);

// 添加数据库上下文
builder.Services.AddDbContext<BlogDbContext>(
    options =>
    {
        var connStr = builder.Configuration.GetConnectionString("postgresql");

        options.UseNpgsql(connStr);
    }
);

// 添加 AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// 添加文章、回复服务
builder.Services.AddScoped<IArticlesService, ArticlesService>();
builder.Services.AddScoped<IRepliesService, RepliesService>();

builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
    options.AddPolicy("Open", builder =>
        builder.AllowAnyHeader()
            .AllowAnyOrigin()
        )
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // 开发环境
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Open");

app.MapControllers();

app.Run();
