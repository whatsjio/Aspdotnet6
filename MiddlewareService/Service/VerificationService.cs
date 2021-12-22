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

        public async Task<Message<string>> RefreshToken(string token) {
            var client = _clientFactory.CreateClient();
            var tokenResponse = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = VerficationConfig.ApiHost + VerficationConfig.RefreshUrl,
                ClientId = VerficationConfig.Clientid,
                ClientSecret = VerficationConfig.ClientSecret,
                RefreshToken = token
            });

            var result = new Message<string>();
            return result;
        }

        /// <summary>
        /// 获取token验证信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public async Task<Message<Tokenresult>> GetToken(string username,string password) {
            //暂时不用redis缓存
            var client = _clientFactory.CreateClient();
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = VerficationConfig.ApiHost + VerficationConfig.Gettoken,
                ClientId = VerficationConfig.Clientid,
                ClientSecret = VerficationConfig.ClientSecret,
                Scope = "openid api",
                UserName = username,
                Password = password,
            });
            if (tokenResponse.IsError) { 
            
            
            }
            return new Message<Tokenresult>();
        }
    }
}
