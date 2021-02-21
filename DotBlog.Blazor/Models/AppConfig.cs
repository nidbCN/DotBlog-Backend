using System.Collections.Generic;
using AntDesign.Pro.Layout;

namespace DotBlog.Blazor.Models
{
    public class AppConfig
    {
        public IEnumerable<MenuDataItem> MenuItems { get; set; }
        public string ApiUrl { get; set; }

    }
}
