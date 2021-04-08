# DotBlog-Backend

基于 .NET 5.0 的博客程序后端

## 功能

> 后端尽量遵循了 RESTful API

1. 新建文章
2. 新建评论
3. 更新文章
4. 更新评论
5. 点赞文章
6. 增加阅读文章数
7. 点赞评论
8. 更改文章
9. 删除文章
10. 删除评论

## TODO

1. 添加认证系统
2. 迁移到框架
3. 使用SqlSugar替换EF Core

## 设置

> 注意，MySql EF 8.0.22不兼容，暂时无法使用！

`appsettings.json` :

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "DataBase": "MySql",
  "ConnectionStrings": {
    "SqLite": "Data Source=DotBlog.db; ",
    "MySql":"Server=myServerAddress;Port=1234;Database=myDataBase;Uid=myUsername;Pwd=myPassword;",
    "PostgreSql":"Provider=PostgreSQL OLE DB Provider;Data Source=myServerAddress;location=myDataBase;User ID=myUsername;password=myPassword;timeout=1000;"
  },
  "AppConfig": {
    "BaseUrl": "/DotBlog/",
    "PasswordSalt": "THIS IS A SALT"
  }
}

```

## 使用

如果你敢用，那真的太好不过了。同时也欢迎 Pr 项目，或者开发自己的前端。
API 请克隆编译项目并 debug 运行后，访问 swagger 页面。

### 注意
`build.sh` 将默认发布适用于 `Ubuntu 20.04 x64` 的二进制文件和 `Dockerfile` 。其余操作系统请自行更改参数。
`build-docker.sh` 将默认发布标签为 `DotBlog-Server:v1.3` 的镜像并且开放5003端口。

## GPLv3

![GPLv3 logo](https://www.gnu.org/graphics/gplv3-with-text-136x68.png)

> Free as in Freedom
