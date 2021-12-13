using DateModel.EnumList;
using DateModel.VerfyModel;
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

        public VerificationService(IConfiguration configuration,ILogger logger, RedisHelper redisHelper)
        {
            _configuration = configuration;
            _logger= logger;
            _redishelp = redisHelper;
        }

        public async Task<Message<string>> RefreshToken(string token) {
            var hander = new AnyMessageHander(EHttpType.POST);
            var postdata = new Dictionary<string, string>() {
                {"client_id",VerficationConfig.Clientid},
                {"client_secret",VerficationConfig.ClientSecret},
                {"grant_type","refresh_token"},
                {"refresh_token",token}
            };
            hander.SetFormContent(postdata);
            var getsend=await HttpHelper.AsyncSend(VerficationConfig.ApiHost + VerficationConfig.RefreshUrl, hander, _logger);
            var result = new Message<string>(getsend.Success, getsend.Message, getsend.Result);
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
            var hander = new AnyMessageHander(EHttpType.POST);
            var postdata = new Dictionary<string, string>() {
                {"client_id",VerficationConfig.Clientid},
                {"client_secret",VerficationConfig.ClientSecret},
                {"grant_type","password"},
                {"userName",username},
                {"password",password}
            };
            hander.SetFormContent(postdata);
            var getsend = await HttpHelper.AsyncSend(VerficationConfig.ApiHost + VerficationConfig.Gettoken, hander, _logger);
            if (getsend.Success) {
                var tokenresult = JsonConvert.DeserializeObject<JToken>(getsend.Result);
                var token = tokenresult.GetTrueValue<string>("access_token");
                //验证是否获取到token
                if(string.IsNullOrEmpty(token)) return new Message<Tokenresult>(false, tokenresult.GetTrueValue<string>("error_description"));
                var data = JsonConvert.DeserializeObject<Tokenresult>(getsend.Result);
                return new Message<Tokenresult>(true, "成功", data);
            }
            else {
                return new Message<Tokenresult>(false, getsend.Message);
            }
        }
    }
}
