using Microsoft.EntityFrameworkCore;

using PlatData.SysTable;

namespace PlatData
{

    /// <summary>
    /// Db实体类
    /// </summary>
    public class DbTContext : DbContext
    {


        public DbTContext(DbContextOptions<DbTContext> options) : base(options)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// 创建表配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //显示声明表信息
            //modelBuilder.Entity<SysAdmin>().HasOne(a => a.PassWord);
        }


        #region 系统表
        /// <summary>
        /// 管理员
        /// </summary>
        public DbSet<SysAdminGroup> SysAdminGroup { get; set; }

        /// <summary>
        /// 系统账号
        /// </summary>
        public DbSet<SysAdmin> SysAdmin { get; set; }
        /// <summary>
        /// 系统账号日志表
        /// </summary>
        public DbSet<SysAdminLog> SysAdminLog { get; set; }
        /// <summary>
        /// 系统菜单
        /// </summary>
        public DbSet<SysMenu> SysMenu { get; set; }
        /// <summary>
        /// 用户权限表
        /// </summary>
        public DbSet<SysAdminMenu> SysAdminMenu { get; set; }
        #endregion


    }
}