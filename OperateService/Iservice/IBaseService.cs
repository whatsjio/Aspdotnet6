using DateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperateService.Iservice
{
    public interface IBaseService
    {
        /// <summary>
        /// 接口消息
        /// </summary>
        public Message MessageDate { get; set; }

      
    }

    public interface IBaseService<T> {
        /// <summary>
        /// 通用接口消息
        /// </summary>
        Message<T> GeMessageDate { get; set; }
    }

}
