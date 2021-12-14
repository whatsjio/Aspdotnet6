
using Microsoft.Extensions.Configuration;

namespace DateModel.ModelHelp
{
    /// <summary>
    /// 配置文件读取
    /// </summary>
    public static class IndependentConfiguration
    {
        /// <summary>
        /// 配置接口
        /// </summary>
        public static IConfiguration Configuration { get; }

        /// <summary>
        /// 静态构造
        /// </summary>
        static IndependentConfiguration() {
            var _enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string _path = $"appsettings{(_enviroment??"."+ _enviroment)}.json";
            var conf = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json",optional:true,reloadOnChange:true)
                .Build();
            Configuration = conf;
        }

        #region 获取配置值
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            return Configuration[key];
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetValue<T>(string key)
        {
            return Configuration.GetValue<T>(key);
        }
        #endregion

        #region 获取节点信息
        /// <summary>
        /// 获取节点信息
        /// </summary>
        /// <param name="key">节点名称</param>
        /// <returns></returns>
        public static IConfigurationSection GetSelction(string key)
        {
            return Configuration.GetSection(key);
        } 
        #endregion

    }
}
