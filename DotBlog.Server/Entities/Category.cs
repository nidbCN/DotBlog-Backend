using System;
using System.Collections.Generic;

namespace DotBlog.Server.Entities
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
