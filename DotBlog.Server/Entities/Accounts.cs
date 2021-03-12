namespace DotBlog.Server.Entities
{
    public class Accounts
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Mail { get; set; }
        
        /// <summary>
        /// 电话号
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// 密码
        /// </summary>
        public string PasswordHash { get; set; }
    }
}
