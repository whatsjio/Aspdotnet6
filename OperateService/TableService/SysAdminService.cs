using OperateService.Iservice;
using OperateService.ITableService;
using OperateService.Service;
using PlatData;
using PlatData.SysTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperateService.TableService
{
    public class SysAdminService : BaseOperateDbService<SysAdmin>, ISysAdmin
    {
        public SysAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
