using DateModel;
using OperateService.Iservice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperateService.Service
{
    /// <summary>
    /// 基础服务类
    /// </summary>
    public abstract class BaseService : IBaseService
    {
        /// <summary>
        /// 
        /// </summary>
        private Message _message;

        /// <summary>
        /// 消息信息
        /// </summary>
        public virtual Message MessageDate { get => _message; set => _message=value; }
      

        public BaseService()
        {
            _message=new Message();
        }
    }

    public abstract class BaseService<T> : IBaseService<T> 
    {
        private Message<T> _gemessage;

        /// <summary>
        /// 通用消息
        /// </summary>
        public virtual Message<T> GeMessageDate { get => _gemessage; set => _gemessage = value; }
        public BaseService()
        {
            _gemessage = new Message<T>();
        }
    }
}
