using System.ComponentModel.DataAnnotations.Schema;

namespace PlatData.SysTable
{
    [Table("SysAdminGroup")]
    public class SysAdminGroup: SysObject
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Comment("名称")]
        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Comment("排序")]
        public int Sort { get; set; }


        /// <summary>
        /// 关联的客户
        /// </summary>
        public virtual List<SysAdmin> SysAdminList { get; set; }
        /// <summary>
        /// 关联菜单信息
        /// </summary>
        public virtual SysAdminMenu MenuList { get; set; }
        public SysAdminGroup()
        {
            Sort = 99;
            SysAdminList = new List<SysAdmin>();
        }
    }
}
