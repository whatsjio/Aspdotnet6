using System.ComponentModel.DataAnnotations.Schema;

namespace PlatData.SysTable
{
    [Table("SysAdminGroup")]
    public class SysAdminGroup: SysObject
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// true启用；false禁用
        /// </summary>
        public bool IsDisable { get; set; }
        /// <summary>
        /// 关联的客户
        /// </summary>
        public virtual List<SysAdmin> SysAdminList { get; set; }
        /// <summary>
        /// 当前的菜单
        /// </summary>
        public virtual List<SysAdminMenu> MenuList { get; set; }
        public SysAdminGroup()
        {
            Sort = 99;
            IsDisable = true;
            SysAdminList = new List<SysAdmin>();
            MenuList = new List<SysAdminMenu>();
        }
    }
}
