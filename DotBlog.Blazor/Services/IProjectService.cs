using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign.Pro.Layout;

namespace DotBlog.Blazor.Services
{
    public interface IProjectService
    {
        IEnumerable<MenuDataItem> GetMenuData();
    }
}
