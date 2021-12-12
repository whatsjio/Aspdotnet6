using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DateModel.VerfyModel
{
    /// <summary>
    /// 验证配置
    /// </summary>
    public static class VerficationConfig
    {
        /// <summary>
        /// 刷新token地址
        /// </summary>
        public static string RefreshUrl => "/connect/token";

        /// <summary>
        /// Api地址
        /// </summary>
        public static string ApiHost { get; }

        static VerficationConfig()
        {
            ApiHost = new ConfigurationManager()["Appraisalurl"];
        }
    }
}
