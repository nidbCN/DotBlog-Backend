using Masuit.Tools;
using Masuit.Tools.Security;

namespace DotBlog.Server.Helper;

public abstract class EncryptPassword
{
    public static string EncryptPasswordWithSalt(string plaintext, string salt) =>
        plaintext.MDString3(salt.IsNullOrEmpty() ? "Default Salt." : salt);
}
