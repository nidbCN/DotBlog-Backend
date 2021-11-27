namespace DotBlog.Server.Dto.QueryModel
{
    public class ArticleGetDtoParameters
    {
        private const uint MAX_SIZE = 20;

        #region 私有字段

        private uint size;
        #endregion

        #region 公有属性

        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public uint Page { get; set; }

        /// <summary>
        /// 页含量
        /// </summary>
        public uint Size
        {
            get => size;
            set => size = value > MAX_SIZE ? MAX_SIZE : value;
        }

        #endregion
    }
}
