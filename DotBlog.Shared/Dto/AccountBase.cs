namespace DotBlog.Shared.Dto;

public abstract class AccountBase
{
    /// <summary>
    /// 认证身份所用的标识
    /// </summary>
    public string Identification { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }
}
