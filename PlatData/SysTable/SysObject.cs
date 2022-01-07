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
        [Column(Order = 0,TypeName = "varchar(255)")]
        public string Id { get; set; }

        //Order定义列排序
        [Column(Order = 100)]
        /// <summary>
        /// 数据创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }


        [Comment("上次更新时间")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column(Order = 99)]
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// 是否逻辑删除 true:是 false:否
        /// </summary>
        public bool GcRecord { get; set; }


    }
}
