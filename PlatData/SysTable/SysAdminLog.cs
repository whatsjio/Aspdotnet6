using System.ComponentModel.DataAnnotations.Schema;

namespace PlatData.SysTable
{

    [Table("SysAdminLog")]
    public class SysAdminLog:SysObject
    {
        /// <summary>
        /// 类型(登陆、新增、或者其他)
        /// </summary>
        public string? Type { get; set; }
        public string? IP { get; set; }
        public string? Remark { get; set; }

        public string? Controller { get; set; }
        public string? ControllerName { get; set; }
        public string? Action { get; set; }
        public string? ActionName { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string? Path { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string? Param { get; set; }

        [Comment("管理员关联外键")]
        public string? ParentFk { get; set; }
        public virtual SysAdmin Parent { get; set; }
    }
}
