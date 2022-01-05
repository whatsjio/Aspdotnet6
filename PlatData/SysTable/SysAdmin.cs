using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatData.SysTable
{
    /// <summary>
    /// 系统管理员
    /// </summary>
    [Table("SysAdmin")]
    //注释
    [Comment("系统管理员")]
    public class SysAdmin:SysObject
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string NickName { get; set; }

        [Comment("随机key密码加密使用")]
        [Column(TypeName = "varchar(200)")]
        public string Salt { get; set; }
        public string Mobile { get; set; }

        [MaxLength(200)]
        public string Avatar { get; set; }
        public string Sex { get; set; }
        /// <summary>
        /// true启用；false禁用
        /// </summary>
        public bool IsDisable { get; set; }
        /// <summary>
        /// 管理组 这个字段不用管EF 自动创建关联
        /// </summary>
        public virtual SysAdminGroup Parent { get; set; }
        /// <summary>
        /// 日志
        /// </summary>
        public virtual List<SysAdminLog> SysAdminLog { get; set; }
        public SysAdmin()
        {
            SysAdminLog = new List<SysAdminLog>();
            IsDisable = true;
        }
    }
}
