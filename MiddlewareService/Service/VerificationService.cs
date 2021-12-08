using DateModel.EnumList;
using DateModel.VerfyModel;
using Microsoft.Extensions.Configuration;
using MiddlewareService.Iservice;
using NLog;
using OperateService.Service;
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
        public VerificationService(IConfiguration configuration,ILogger logger)
        {
            _configuration = configuration;
            _logger= logger;
        }

        public async Task<Message<string>> RefreshToken(string token) {
            var hander = new AnyMessageHander(EHttpType.POST);
            var postdata = new Dictionary<string, string>() {
                {"client_id","client"},
                {"client_secret","secret"},
                {"grant_type","refresh_token"},
                {"refresh_token",token}
            };
            hander.SetFormContent(postdata);
            var getsend=await HttpHelper.AsyncSend(VerficationConfig.ApiHost + VerficationConfig.RefreshUrl, hander, _logger);
            var result = new Message<string>(getsend.Success, getsend.Message, getsend.Result);
            return result;
        }

    }
}
