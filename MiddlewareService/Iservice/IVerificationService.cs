global using DateModel;
using OperateService.Iservice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareService.Iservice
{
    /// <summary>
    /// 验证服务接口
    /// </summary>
    public interface IVerificationService: IBaseService
    {

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        Task<Message<string>> RefreshToken(string token);
    }
}
