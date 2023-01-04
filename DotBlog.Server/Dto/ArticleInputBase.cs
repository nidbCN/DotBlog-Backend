using DotBlog.Shared.Dto;
using Masuit.Tools.Html;
using System.ComponentModel.DataAnnotations;

namespace DotBlog.Server.Dto;

public abstract class ArticleInputBase : ArticleBase
{
    private string _author = "匿名";
    private string? _category;
    private string? _description;
    private string _title = "未命名";
    private string _content = string.Empty;

    /// <summary>
    /// 是否展示在首页上
    /// </summary>
    public bool IsShown { get; set; }

    /// <summary>
    /// 文章内容
    /// </summary>
    [Display(Name = "文章内容")]
    [Required(ErrorMessage = "{0}这个字段是必填的")]
    public string Content
    {
        get => _content;
        set => _content = value.HtmlSantinizerStandard();
    }

    // 重写父类的字段

    public override string Author
    {
        get => _author;
        set => _author = value.HtmlSantinizerStandard();
    }

    public override string? Category
    {
        get => _category;
        set => _category = value.HtmlSantinizerStandard();
    }

    public override string? Description
    {
        get => _description;
        set => _description = value.HtmlSantinizerStandard();
    }

    public override string Title
    {
        get => _title;
        set => _title = value.HtmlSantinizerStandard();
    }
}
