namespace PlatData.GroupEFConfigure
{
    /// <summary>
    /// 系统管理员属性自定义配置
    /// </summary>
    public class SysAdminEntityTypeConfiguration : IEntityTypeConfiguration<SysAdmin>
    {
        /// <summary>
        /// 如果在类型中使用数据注释会被 Fluent API 配置替代
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<SysAdmin> builder)
        {
            builder.Property(b => b.UserName).IsRequired();
            //组合主键示例
            //builder.HasKey(c => new { c.UserName, c.Id });
        }
    }
}
