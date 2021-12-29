using OperateService.TableInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperateService.TableService
{

    public class SysAdminMenuService : BaseOperateDbService<SysAdminMenu>, ISysAdminMenu
    {
        public SysAdminMenuService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
