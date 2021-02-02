# DotBlog

基于 .NET 5.0 的博客程序

## 功能

> 注意，目前仅完成这部分功能的基本工作，异常处理还没有完成，同时，API 是没有经过测试的。

> 后端尽量遵循了 REST ful API

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

## 使用

如果你敢用，那真的太好不过了。同时也欢迎 Pr 项目，或者开发自己的前端。下面是 Web API。

### 文章

#### 获取文章列表

**GET** `/v1/Articles`

##### 负载

| 名称  | 类型  | 必须 | 传参  | 介绍                                 |
| ----- | ----- | ---- | ----- | ------------------------------------ |
| limit | Int32 | 否   | query | 返回的条数限制，不填或负数则返回全部 |

##### 响应

> 为多个文章实例组成的列表

```json
[
  {
    "articleId": "86168e03-75ac-4260-a07d-ad4b5fd0b896",
    "alias": "test1",
    "title": "test1",
    "description": "This is test1",
    "isShown": true,
    "read": 0,
    "like": 0,
    "postTime": "2021-02-01T18:30:10.921807",
    "resourceUri": "/article/86168e03-75ac-4260-a07d-ad4b5fd0b896"
  },
  {
    "articleId": "0488e78b-91e2-4ed8-a24c-79419f0c15eb",
    "alias": "Hello-Dot-Blog",
    "title": "Hello World 第一篇博客",
    "description": "我的第一篇博客，测试Web API用",
    "isShown": true,
    "read": 0,
    "like": 0,
    "postTime": "2021-02-01T18:57:06.558158",
    "resourceUri": "/article/0488e78b-91e2-4ed8-a24c-79419f0c15eb"
  },
  {
    "articleId": "12b09f54-008e-45cc-930c-102f62d26a55",
    "alias": "hello-world-dot-blog",
    "title": "The first blog",
    "description": "This is the first I post",
    "isShown": true,
    "read": 0,
    "like": 0,
    "postTime": "2021-02-02T01:07:01.2806744",
    "resourceUri": "/article/12b09f54-008e-45cc-930c-102f62d26a55"
  },
  {
    "articleId": "039c68cc-13e6-4f51-b28d-7174759a67d5",
    "alias": "hi",
    "title": "The 2 test",
    "description": "a good test",
    "isShown": true,
    "read": 0,
    "like": 0,
    "postTime": "2021-02-02T01:09:30.4258331",
    "resourceUri": "/article/039c68cc-13e6-4f51-b28d-7174759a67d5"
  }
]
```

#### 新建文章

**POST** `/v1/Article`

##### 负载

| 类型             | 必须 | 传参 | 介绍                |
| ---------------- | ---- | ---- | ------------------- |
| application/json | 是   | body | 文章对象实例的 JSON |

```json
{
  "alias": "hello-world-dot-blog", // 别名，通常用于显示在URL
  "title": "The first blog", // 标题，文章的标题
  "description": "This is the first I post", // 介绍，对于文章内容的简略介绍
  "isShown": true, // 可见，是否显示在首页
  // 设置为false仅仅是不在索引里，仍然是公开的！
  "category": "diary", // 分类，文章所属的分类
  "author": "Gaein nidb", // 作者，分支的作者
  "content": "There,I write down my first ..." // 内容，文章的内容，可以考虑Markdown或者HTML
}
```

##### 响应

```json
{
  "articleId": "a6560056-dee1-4402-91c1-495998a4b22a", // 文章Guid
  "alias": "hello-world-dot-blog",
  "title": "The first blog",
  "description": "This is the first I post",
  "isShown": true,
  "read": 0, // 阅读数
  "like": 0, // 点赞数
  "category": "diary",
  "postTime": "2021-02-02T01:13:52.6011051+08:00", // 发布时间
  "author": "Gaein nidb",
  "content": "There,I write down my first ...",
  "resourceUri": "/article/a6560056-dee1-4402-91c1-495998a4b22a", // URI地址
  "replies": null // 回复（有回复时是列表）
}
```

#### 新建评论

/v1/Article/{articleId}/reply

##### 负载

c599a651-5e7d-45f4-a3db-bb734829484c

```json
{
  "UserPlatform": "Windows 10",
  "UserExplore": "FireFox",
  "AvatarUrl": "/avatar/user",
  "ReplyTo": "253004a9-fd5d-441b-b66f-778395fbc5e6",
  "author": "reply user",
  "content": "MarkDown!",
  "Link": "https://usersite.com",
  "Mail": "user@outlook.com"
}
```

#### 获取文章实例

**GET** `/v1/Article/{articleId}`

##### 负载

| 名称      | 类型 | 必须 | 传参  | 介绍          |
| --------- | ---- | ---- | ----- | ------------- |
| articleId | Guid | 是   | route | 获取的文章 ID |

##### 响应

HTTP 200 获取成功

```json
{
  "code": 0,
  "message": "Success",
  "count": 0,
  "value": {
    "articleId": "c599a651-5e7d-45f4-a3db-bb734829484c",
    "title": "The first blog",
    "description": "This is the first I post",
    "read": 0,
    "like": 0,
    "category": "diary",
    "postTime": "2021-02-02T11:25:47.2030793",
    "author": null,
    "content": "There,I write down my first ...",
    "resourceUri": "/article/c599a651-5e7d-45f4-a3db-bb734829484c"
  }
}
```

#### 更新文章

##### 负载

c599a651-5e7d-45f4-a3db-bb734829484c

```
{
  "alisa": "new-post",
  "title": "The first blog, new title",
  "description": "RE This is the first I post",
  "like": 100,
  "category": "diary2",
  "isShown": false,
  "postTime": "2021-02-02T11:25:47.2030793",
  "author": "Gaein nidb",
  "content": "I rewrite this and set isShown to false",
}
```

#### 更新文章点赞数

**PATCH** `/v1​/Article​/{articleId}​/Like`

##### 负载

articleId

##### 响应

#### 更新文章阅读数

**PATCH** `/v1​/Article​/{articleId}​/Read`

##### 负载

articleId

##### 响应

#### 删除文章

**DELETE** `/v1/Article/{articleId}`

##### 负载

| 名称      | 类型 | 必须 | 传参  | 介绍          |
| --------- | ---- | ---- | ----- | ------------- |
| articleId | Guid | 否   | route | 删除的文章 ID |

##### 响应

HTTP 204 删除成功

## 计划

1. 更新内部异常处理，使程序更加稳定；
2. 返回的 HTTP 状态码和相关信息更恰当；
3. 优化代码，补写注释；
4. 优化控制器结构；
5. 使用 VueJs 或 Blazor 构建前端页面
6. 支持多种数据库类型
7. And More ...

## GPLv3

![GPLv3 logo](https://www.gnu.org/graphics/gplv3-with-text-136x68.png)

> Free as in Freedom
