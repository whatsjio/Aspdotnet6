using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatData.GroupEFConfigure
{
    internal class SysMenuEntityTypeConfiguration : IEntityTypeConfiguration<SysMenu>
    {
        public void Configure(EntityTypeBuilder<SysMenu> builder)
        {
            //设置阴影属性可选
            builder.HasOne(t => t.Parent).WithMany(s => s.ChildrenList).IsRequired(false);
        }
    }
}
