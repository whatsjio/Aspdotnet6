namespace PlatData.GroupEFConfigure
{
    /// <summary>
    /// 系统管理员属性自定义配置
    /// </summary>
    internal class SysAdminEntityTypeConfiguration : IEntityTypeConfiguration<SysAdmin>
    {
        /// <summary>
        /// 如果在类型中使用数据注释会被 Fluent API 配置替代
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<SysAdmin> builder)
        {
            builder.Property(b => b.UserName).IsRequired();
            builder.HasOne(t => t.Parent).WithMany(c => c.SysAdminList).IsRequired(false);
            //设置备用键默认值
            //builder.Property(b=>b.SysAdminLogfk).HasDefaultValue(Guid.NewGuid().ToString());
            //组合主键示例
            //builder.HasKey(c => new { c.UserName, c.Id });
        }
    }
}
