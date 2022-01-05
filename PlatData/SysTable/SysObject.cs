using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column(Order = 0)]
        public string Id { get; set; }

        //Order定义列排序
        [Column(Order = 100)]
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
