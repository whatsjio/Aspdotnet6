using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateModel.VerfyModel
{
    /// <summary>
    /// Acctoken存储模型
    /// </summary>
    public class UserAccestokenModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// token
        /// </summary>
        public string AccessToken { get; set; }
    }
}
