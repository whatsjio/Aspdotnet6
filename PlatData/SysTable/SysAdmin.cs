﻿using System.ComponentModel.DataAnnotations;
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
        [Column(TypeName = "nvarchar(200)")]
        public string UserName { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string PassWord { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? NickName { get; set; }

        [Comment("随机key密码加密使用")]
        [Column(TypeName = "varchar(200)")]
        public string Salt { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? Mobile { get; set; }

        [MaxLength(200)]
        public string? Avatar { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? Sex { get; set; }

        /// <summary>
        /// 管理组关联外键 如果不指定外键则自动适用阴影属性 ParentId （阴影属性需用Fluent API显示声明IsRequired）
        /// </summary>
        public virtual SysAdminGroup Parent { get; set; }


        /// <summary>
        /// 日志
        /// </summary>
        public virtual List<SysAdminLog> SysAdminLog { get; set; }

        /// <summary>
        /// 日志关联备用键（必填）类似于主键的备用
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string SysAdminLogfk { get; set; }

        public SysAdmin()
        {
            SysAdminLog = new List<SysAdminLog>();
            //SysAdminLogfk = Guid.NewGuid().ToString();
        }
    }
}
