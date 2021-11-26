using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotBlog.Server.Data;
using DotBlog.Server.Entities;

namespace DotBlog.Server.Repositories
{
    public class ArticlesRepository : IArticlesRepository
    {
        #region 私有字段
        private readonly DotBlogDbContext _context;

        private readonly ILogger<ArticlesRepository> _logger;
        #endregion

        #region 构造函数
        public ArticlesRepository(DotBlogDbContext context, ILogger<ArticlesRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        #endregion

        #region 公共方法
        public async Task<IList<Article>> FindAllAsync(Predicate<Article> match)
            => await _context.Articles.Where(x => match(x)).ToListAsync();

        public async Task<Article> FindAsync(int id)
            => await _context.Articles.FirstOrDefaultAsync(x => x.ArticleId == id);

        public async Task<IList<Article>> GetAllAsync()
            => await _context.Articles.ToListAsync();

        public async Task<IList<Article>> GetAsync(int offset, int count)
            => await _context.Articles.Skip(offset).Take(count).ToListAsync();

        public void Remove(Article article)
            => _context.Articles.Remove(article);

        public void RemoveAll(Predicate<Article> match)
        {
            var searchResult = FindAllAsync(match).Result;
            foreach (var article in searchResult)
            {
                _context.Articles.Remove(article);
            }

            _context.SaveChanges();
        }

        public void RemoveAt(int id)
            => RemoveAll(x => x.ArticleId == id);
        #endregion
    }
}
