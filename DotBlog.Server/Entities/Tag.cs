using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotBlog.Server.Entities
{
    public class Tag
    {
        public Article Article { get; set; }
        public Guid TagId { get; set; }
        public string Name { get; set; }


    }
}
