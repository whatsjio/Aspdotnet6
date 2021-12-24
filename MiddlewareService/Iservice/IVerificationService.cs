global using DateModel;
using DateModel.VerfyModel;
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
        /// <param name="token">用户token</param>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        Task<Message<UserAccestokenModel>> RefreshToken(string token,string username);

        /// <summary>
        /// 获取token验证信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Task<Message<UserAccestokenModel>> GetToken(string username, string password);
    }
}
