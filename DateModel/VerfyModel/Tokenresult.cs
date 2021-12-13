using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateModel.VerfyModel
{
    public class Tokenresult
    {
        /// <summary>
        /// 获取的token
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// token过期时间
        /// </summary>
        public int expires_in { get; set; }
        /// <summary>
        /// token类型
        /// </summary>
        public string token_type { get; set; }
        /// <summary>
        /// 刷新token
        /// </summary>
        public string refresh_token { get; set; }
        /// <summary>
        /// 鉴权范围
        /// </summary>
        public string scope { get; set; }

        /// <summary>
        /// 错误标识
        /// </summary>
        public string error { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string error_description { get; set; }
    }
}
