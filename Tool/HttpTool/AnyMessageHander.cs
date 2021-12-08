using DateModel.EnumList;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Tool.HttpTool
{
    /// <summary>
    /// Http请求定制处理类
    /// </summary>
    public class AnyMessageHander : HttpClientHandler
    {
        /// <summary>
        /// 日志组件
        /// </summary>
        private ILogger? _logger;
        /// <summary>
        /// 定制请求头
        /// </summary>
        public Dictionary<string, string> HeaderDictionary { get; }

        /// <summary>
        /// 提交数据
        /// </summary>
        public string Postdata { get; set; }

        /// <summary>
        /// 请求主体
        /// </summary>
        public HttpContent PosthttpContent { get; set; }

        /// <summary>
        /// Http请求方法
        /// </summary>
        public HttpMethod SendMethod { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httptype">Http请求类型</param>
        public AnyMessageHander(EHttpType httptype)
        {
            SendMethod = HttpMethod.Get;
            switch (httptype)
            {
                case EHttpType.GET:
                    SendMethod = HttpMethod.Get;
                    break;
                case EHttpType.POST:
                    SendMethod = HttpMethod.Post;
                    break;
                case EHttpType.DELETE:
                    SendMethod = HttpMethod.Delete;
                    break;
                case EHttpType.PUT:
                    SendMethod = HttpMethod.Put;
                    break;
            }
        }

        #region 设置httpcontentForm提交方式
        /// <summary>
        /// 设置httpcontentForm提交方式
        /// </summary>
        /// <param name="valuecollection"></param>
        public virtual void SetFormContent(Dictionary<string, string> valuecollection)
        {
            Postdata = JsonConvert.SerializeObject(valuecollection);
            PosthttpContent = new FormUrlEncodedContent(valuecollection);
        }
        #endregion

        #region 设置httpcontent字符串请求体
        /// <summary>
        /// 设置httpcontent字符串请求体
        /// </summary>
        public virtual void SetContent(string postdata, string contentType)
        {
            Postdata = postdata;
            PosthttpContent = new StringContent(postdata ?? "");
            PosthttpContent.Headers.ContentType = new MediaTypeHeaderValue(contentType)
            {
                CharSet = "utf-8"
            };
        }
        #endregion

        /// <summary>
        /// 设置日志记录组件
        /// </summary>
        /// <param name="logger"></param>
        public virtual void SetLog(ILogger logger) {
            _logger = logger;
        }

        /// <summary>
        /// 提交请求
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                request.Method = SendMethod;
                if (request.Method != HttpMethod.Get)
                {
                    if (PosthttpContent == null)
                    {
                        _logger?.Info($"{SendMethod.Method}请求异常：未设置Content主体", this);
                        var repsonse = new HttpResponseMessage(HttpStatusCode.BadRequest);
                        return Task.FromResult(repsonse);
                    }
                    request.Content = PosthttpContent;
                }
                if (HeaderDictionary != null && HeaderDictionary.Any())
                {
                    foreach (var keyValuePair in HeaderDictionary)
                    {
                        request.Headers.Add(keyValuePair.Key, keyValuePair.Value);
                    }
                }
            }
            catch (Exception e)
            {
                _logger?.Error($"{SendMethod.Method}请求异常:{JsonConvert.SerializeObject(e)}", this);
                var repsonse = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return Task.FromResult(repsonse);
            }
            return base.SendAsync(request, cancellationToken);
        }


    }
}
