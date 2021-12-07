using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateModel.GenerModel
{
    /// <summary>
    /// Http结果返回
    /// </summary>
    public class HttpPostresult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 返回结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 结果返回
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="result">结果</param>
        public HttpPostresult(bool success, string result)
        {
            Success = success;
            Result = result;
        }
    }
}
