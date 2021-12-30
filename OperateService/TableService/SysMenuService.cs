using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperateService.TableService
{
    public class SysMenuService : BaseOperateDbService<SysMenu>, ISysMenu
    {
        public SysMenuService(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }
    }
}
