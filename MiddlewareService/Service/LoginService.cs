
using OperateService.ITableService;

namespace MiddlewareService.Service
{
    public class LoginService : BaseService, ILoginService
    {
        private readonly ISysAdmin _isysadmin;

        /// <summary>
        /// DI
        /// </summary>
        /// <param name="sysAdmin"></param>
        public LoginService(ISysAdmin sysAdmin)
        {
            _isysadmin=sysAdmin;
        }

        public void GetLogin() { 
        

        
        }


    }
}
