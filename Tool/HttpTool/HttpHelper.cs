using DateModel.GenerModel;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tool.HttpTool
{
    /// <summary>
    /// Http帮助类
    /// </summary>
    public static class HttpHelper
    {

        /// <summary>
        /// http请求
        /// </summary>
        /// <typeparam name="T">请求处理类型</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="anyMessageHander">定制处理</param>
        /// <param name="logger">日志组件</param>
        /// <param name="writeresponse">是否开启日志写入返回结果 默认开启</param>
        /// <param name="name">调用者名称</param>
        /// <returns></returns>
        public static async Task<HttpPostresult> AsyncSend<T>(string url, T anyMessageHander, ILogger logger, bool writeresponse = true,
          [CallerMemberName] string name = null) where T : AnyMessageHander
        {
            var resultmodel = new HttpPostresult(false, "");
            if (anyMessageHander == null) return new HttpPostresult(false, "缺失处理方法");
            var verifyheadstr = anyMessageHander.HeaderDictionary != null ? JsonConvert.SerializeObject(anyMessageHander.HeaderDictionary) : "";
            var methodname = anyMessageHander.SendMethod.Method;
            var asyncguid = Guid.NewGuid().ToString();
            var errorlog = $"{asyncguid}|异步{methodname}请求:{name}|请求详情:url:{url}|头部参数:{verifyheadstr}|提交参数{anyMessageHander.Postdata}";
            logger.Info($"{asyncguid}|异步{methodname}请求:{name}|请求详情:url:{url}|头部参数:{verifyheadstr}");
            try
            {
                if (url.StartsWith("https"))
                {
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                }
                using (HttpClient httpClient = new HttpClient(anyMessageHander))
                {
                    //提交方法以管道处理为准
                    var requerthttp = new HttpRequestMessage(HttpMethod.Post, url);
                    HttpResponseMessage response = await httpClient.SendAsync(requerthttp).ConfigureAwait(false);
                    var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        resultmodel.Success = true;
                        resultmodel.Result = result;
                        logger.Info($"{asyncguid}|异步{methodname}请求:{name}响应成功|响应信息:{(writeresponse ? result : "未开启写入详情")}");
                    }
                    else
                    {
                        var erroresponCode = $"{methodname}响应{response.StatusCode}失败";
                        resultmodel.Result = erroresponCode;
                        logger.Info($"{errorlog}|响应代码:{erroresponCode}|响应信息:{result}");
                    }
                }
            }
            catch (Exception e)
            {
                resultmodel.Result = $"{methodname}请求异常:{e.Message}";
                logger.Error($"{errorlog}|{methodname}请求异常:{JsonConvert.SerializeObject(e)}");
            }
            return resultmodel;
        }
    }
}
