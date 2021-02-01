namespace DotBlog.Server.Models
{
    public class ReturnUniversally
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="returnValue">要返回的对象</param>
        public ReturnUniversally(object returnValue)
        {
            Value = returnValue;
        }
        public uint Code { get; set; } = 0;
        public string Message { get; set; } = "Success";
        public uint Count { get; set; } = 0;
        public object Value { get; set; } = null;
    }
}
