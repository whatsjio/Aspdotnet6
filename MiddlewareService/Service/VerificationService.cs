using DateModel.EnumList;
using DateModel.VerfyModel;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using MiddlewareService.Iservice;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using OperateService.Service;
using StackExchange.Redis;
using Tool;
using Tool.HttpTool;

namespace MiddlewareService.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class VerificationService: BaseService, IVerificationService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly RedisHelper _redishelp;
        private readonly IHttpClientFactory _clientFactory;

        public VerificationService(IConfiguration configuration,ILogger logger, RedisHelper redisHelper, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _logger= logger;
            _redishelp = redisHelper;
            _clientFactory = httpClientFactory;
        }


        #region 刷新token
        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="token">用户token</param>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public async Task<Message<UserAccestokenModel>> RefreshToken(string token,string username)
        {
            var getinfo =await _redishelp.StringGetAsync<Tokenresult>(username);
            if(getinfo==null)
                return new Message<UserAccestokenModel>(false, "找不到用户登录信息");
            if(getinfo.access_token!= token)
                return new Message<UserAccestokenModel>(false, "用户token异常");
            var client = _clientFactory.CreateClient();
            var tokenResponse = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = VerficationConfig.ApiHost + VerficationConfig.RefreshUrl,
                ClientId = VerficationConfig.Clientid,
                ClientSecret = VerficationConfig.ClientSecret,
                RefreshToken = token
            });

            var result = new Message<UserAccestokenModel>();
            return result;
        } 
        #endregion



        #region 获取token验证信息
        /// <summary>
        /// 获取token验证信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public async Task<Message<UserAccestokenModel>> GetToken(string username, string password)
        {
            //ip防御代码
            var client = _clientFactory.CreateClient();
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = VerficationConfig.ApiHost + VerficationConfig.Gettoken,
                ClientId = VerficationConfig.Clientid,
                ClientSecret = VerficationConfig.ClientSecret,
                Scope = "openid api offline_access",
                UserName = username,
                Password = password,
            });
            if (tokenResponse.IsError)
            {
                return new Message<UserAccestokenModel>(false, tokenResponse.ErrorDescription);
            }
            //存储结果
            var gettoken = new Tokenresult()
            {
                access_token = tokenResponse.AccessToken,
                expires_in = tokenResponse.ExpiresIn,
                refresh_token = tokenResponse.RefreshToken
            };
            await _redishelp.StringSetAsync(username, gettoken);
            return new Message<UserAccestokenModel>(true,"", 
            new UserAccestokenModel()
            {
                UserName = username,
                AccessToken = tokenResponse.AccessToken
            });
        } 
        #endregion
    }
}
