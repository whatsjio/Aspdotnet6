using System.ComponentModel.DataAnnotations.Schema;

namespace PlatData.SysTable
{
    /// <summary>
    /// 组织菜单信息
    /// </summary>
    [Table("SysAdminMenu")]
    public class SysAdminMenu : SysObject
    {
        public SysAdminMenu()
        {
            Menus=new List<SysMenu>();
        }

        /// <summary>
        /// 所属组织
        /// </summary>
        public virtual SysAdminGroup ByGroup { get; set; }

        /// <summary>
        /// 所拥有的菜单
        /// </summary>
        public virtual List<SysMenu> Menus { get; set; }
    }
}
