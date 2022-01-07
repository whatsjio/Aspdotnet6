
namespace OperateService.TableService
{
    public class SysAdminGroupService : BaseOperateDbService<SysAdminGroup>, ISysAdminGroup
    {
        public SysAdminGroupService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

    }
}
