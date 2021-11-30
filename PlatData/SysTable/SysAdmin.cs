using System.ComponentModel.DataAnnotations.Schema;

namespace PlatData.SysTable
{
    [Table("SysAdmin")]
    public class SysAdmin:SysObject
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string NickName { get; set; }
        /// <summary>
        /// 随机key密码加密使用
        /// </summary>
        public string Salt { get; set; }
        public string Mobile { get; set; }
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
