using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool
{
    /// <summary>
    /// Json工具类
    /// </summary>
    public static class JsonTool
    {
        /// <summary>
        /// 获取Jtoken数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jToken">jToken</param>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public static T GetTrueValue<T>(this JToken jToken, string key)
        {
            if (jToken[key] == null) return default(T);
            return jToken[key].Value<T>();
        }
    }
}
