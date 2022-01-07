namespace OperateService.TableService
{
    public class SysAdminService : BaseOperateDbService<SysAdmin>, ISysAdmin
    {
        public SysAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
