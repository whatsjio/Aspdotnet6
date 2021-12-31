using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PlatData.SysTable
{
    /// <summary>
    /// 数据表基类
    /// </summary>
    //[Index(nameof(CreateTime), IsUnique = true)]
    public abstract class SysObject
    {
        public SysObject()
        {
            Id=Guid.NewGuid().ToString();
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 数据创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否逻辑删除 true:是 false:否
        /// </summary>
        public bool GcRecord { get; set; }


    }
}
