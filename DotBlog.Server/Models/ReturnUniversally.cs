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

        public ReturnUniversally(object returnValue,uint count)
        {
            Value = returnValue;
            Count = count;
        }

        public ReturnUniversally(object returnValue, uint count, uint code, string message)
        {
            Value = returnValue;
            Count = count;
            Code = code;
            Message = message;
        }

        public uint Code { get; set; } = 0;
        public string Message { get; set; } = "Success";
        public uint Count { get; set; } = 0;
        public object Value { get; set; } = null;
    }
}
