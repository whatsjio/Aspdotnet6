using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatData.GroupEFConfigure
{
    internal class SysAdminLogTypeConfiguration : IEntityTypeConfiguration<SysAdminLog>
    {
        public void Configure(EntityTypeBuilder<SysAdminLog> builder)
        {
            //如果想要外键引用主键外的属性，可使用 Fluent API 为关系配置主体键属性。 配置为主体键的属性将自动设置为备选键。
            //级联删除 OnDelete
            builder.HasOne(t => t.Parent).WithMany(c => c.SysAdminLog).HasForeignKey(t => t.ParentFk).
                HasPrincipalKey(t => t.SysAdminLogfk);
                //.OnDelete(DeleteBehavior.Cascade)
        }
    }
}
