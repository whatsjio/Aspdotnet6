using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateModel.VerfyModel
{
    public class Usermodel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string password { get; set; }
    }
}
