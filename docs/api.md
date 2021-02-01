# RESTful HTTP API 文档

https://api.example.cn

## 读取相关（使用 HTTP Get）

### 获取所有文章的列表

https://api.example.cn/v1/articles

#### 可选参数

int : limit

### 获取文章详情

https://api.example.cn/v1/article/{articleId}

### 获取文章评论列表

https://api.example.cn/v1/article/{articleId}/replies/{replyId}


## 更新相关（使用 HTTP Patch）

### 更新文章详情

https://api.example.cn/v1/article/{articleId}

### 点赞

https://api.example.cn/v1/article/{articleId}/like

### 阅读

https://api.example.cn/v1/article/{articleId}/read

### 更新评论详情

https://api.example.cn/v1/article/{articleId}/reply/{replyId}

### 点赞

https://api.example.cn/v1/article/{articleId}/reply/{replyId}/like

## 新建相关（使用 HTTP Post）

### 新建文章

https://api.example.cn/v1/article

### 添加评论

https://api.example.cn/v1/article/{articleId}/reply

## 删除相关（使用 HTTP Delete）

### 删除文章

https://api.example.cn/v1/article/{articleId}

### 删除所有评论

https://api.example.cn/v1/article/{articleId}/replies

### 删除评论

https://api.example.cn/v1/article/{articleId}/reply/{replyId}