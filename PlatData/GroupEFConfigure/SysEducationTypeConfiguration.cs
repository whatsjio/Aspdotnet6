using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatData.GroupEFConfigure
{
    public class SysEducationTypeConfiguration : IEntityTypeConfiguration<SysEducation>
    {
        public void Configure(EntityTypeBuilder<SysEducation> builder)
        {
            //创建索引器属性
            builder.IndexerProperty<DateTime>("LastUpdateded");
            //阴影属性
            builder.Property<DateTime>("LastUpdatedShadow");
        }
    }
}
