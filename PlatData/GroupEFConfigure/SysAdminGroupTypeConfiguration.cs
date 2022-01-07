namespace PlatData.GroupEFConfigure
{

    internal class SysAdminGroupTypeConfiguration : IEntityTypeConfiguration<SysAdminGroup>
    {

        public void Configure(EntityTypeBuilder<SysAdminGroup> builder)
        {
            builder.HasOne(b => b.MenuList).WithOne(b => b.ByGroup).HasForeignKey<SysAdminMenu>(b=>b.SysAdminKey);
        }
    }
}
