using AntDesign.Pro.Layout;
using DotBlog.Blazor.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace DotBlog.Blazor.Layouts
{
    public partial class BasicLayout
    {
        [Inject] public IProjectService ProjectService { get; set; }

        public IEnumerable<MenuDataItem> MenuData { get; set; } = new List<MenuDataItem>();

        protected override void OnInitialized()
        {
            MenuData = ProjectService.GetMenuData();
            base.OnInitialized();
        }
    }
}
