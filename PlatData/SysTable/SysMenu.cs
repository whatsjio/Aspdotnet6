namespace PlatData.SysTable
{
    [Table("SysMenu")]
    public class SysMenu: SysObject
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Column(TypeName = "nvarchar(80)")]
        public string Title { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string? Url { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Column(TypeName = "nvarchar(80)")]
        public string Name { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string? Icon { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        
        public virtual SysMenu Parent { get; set; }
        /// <summary>
        /// 权重越小越靠前
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 是否能被删除 false能；true不能
        /// </summary>
        public bool isDel { get; set; }
        /// <summary>
        /// 子集
        /// </summary>
        public virtual List<SysMenu> ChildrenList { get; set; }

        public SysMenu()
        {
            Sort = 99;
            isDel = false;
            ChildrenList = new List<SysMenu>();
        }
    }
}
