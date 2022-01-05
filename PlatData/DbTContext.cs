namespace PlatData
{

    /// <summary>
    /// Db实体类
    /// </summary>
    public class DbTContext : DbContext
    {

        /// <summary>
        /// DI实例化
        /// </summary>
        /// <param name="options"></param>
        public DbTContext(DbContextOptions<DbTContext> options) : base(options)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //将EF详细异常引入try-catch 块，可能会导致性能问题
            optionsBuilder.EnableDetailedErrors();
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// 创建表配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //显示声明表信息
            //modelBuilder.Entity<SysAdmin>().HasOne(a => a.PassWord);
            new SysAdminEntityTypeConfiguration().Configure(modelBuilder.Entity<SysAdmin>());
            new SysAdminGroupTypeConfiguration().Configure(modelBuilder.Entity<SysAdminGroup>());
            new SysAdminLogTypeConfiguration().Configure(modelBuilder.Entity<SysAdminLog>());
            new SysEducationTypeConfiguration().Configure(modelBuilder.Entity<SysEducation>());
            //排除模型例子 也可以使用数据注释 [NotMapped] [NotMapped]也可以在属性上用作排除属性
            //modelBuilder.Ignore<SysAdmin>();
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

        /// <summary>
        /// EF教学表
        /// </summary>
        public DbSet<SysEducation> SysEducation { get; set; }
        #endregion


    }
}