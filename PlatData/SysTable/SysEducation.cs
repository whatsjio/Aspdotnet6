namespace PlatData.SysTable
{
    [Table("SysEducation")]
    [Comment("EF教学表")]
    public class SysEducation : SysObject
    {
        private readonly Dictionary<string, object> _data = new Dictionary<string, object>();
        /// <summary>
        /// 索引器属性
        /// 可以通过 EF.Property 静态方法或通过使用 CLR 索引器属性在 LINQ 查询中引用索引器属性。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get => _data[key];
            set => _data[key] = value;
        }
    }
}
