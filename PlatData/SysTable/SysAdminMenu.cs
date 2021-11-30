using System.ComponentModel.DataAnnotations.Schema;

namespace PlatData.SysTable
{
    [Table("SysAdminMenu")]
    public class SysAdminMenu : SysObject
    {
        public virtual SysMenu Menu { get; set; }
        public virtual SysMenu Parent { get; set; }
        public virtual SysAdminGroup AdminGroup { get; set; }
    }
}
