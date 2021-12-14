using DateModel.ModelHelp;

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
        /// 获取token
        /// </summary>
        public static string Gettoken => "/connect/token";

        /// <summary>
        /// 验证客户端id
        /// </summary>
        public static string Clientid { get; }

        /// <summary>
        /// 验证密钥
        /// </summary>
        public static string ClientSecret { get; }

        /// <summary>
        /// Api地址
        /// </summary>
        public static string ApiHost { get; }

        /// <summary>
        /// 静态构造
        /// </summary>
        static VerficationConfig()
        {
            ApiHost = IndependentConfiguration.GetValue("Appraisalurl");
            var sectionauth = IndependentConfiguration.GetSelction("AuthInf:Default");
            Clientid= sectionauth.GetSection("Clientid").Value;
            ClientSecret= sectionauth.GetSection("ClientSecret").Value;
        }
    }
}
