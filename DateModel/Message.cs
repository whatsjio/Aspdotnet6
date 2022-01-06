namespace DateModel
{

    /// <summary>
    /// 消息实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Message<T>
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string MessageStr { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSucess { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 构造默认成功
        /// </summary>
        public Message():this(true, "成功",default(T))
        {

        }

        /// <summary>
        /// 默认成功
        /// </summary>
        /// <param name="data">传递值</param>
        public Message(T data) : this(true, "成功", data) { 

        }



        /// <summary>
        /// 构造消息内容
        /// </summary>
        /// <param name="issuccess">是否成功</param>
        /// <param name="message">返回消息</param>
        public Message(bool issuccess,string message,T data)
        {
            IsSucess = issuccess;
            MessageStr = message;
            Data=data;
        }

        /// <summary>
        /// 构造消息内容
        /// </summary>
        /// <param name="issuccess">是否成功</param>
        /// <param name="message">返回消息</param>
        public Message(bool issuccess, string message):this(issuccess, message,default(T))
        {

        }
    }

    /// <summary>
    /// 接口基础消息
    /// </summary>
    public class Message
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string MessageStr { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSucess { get; set; }


        /// <summary>
        /// 构造默认成功
        /// </summary>
        public Message():this(true, "成功")
        {

        }

        /// <summary>
        /// 构造消息内容
        /// </summary>
        /// <param name="issuccess">是否成功</param>
        /// <param name="message">返回消息</param>
        public Message(bool issuccess, string message)
        {
            IsSucess = issuccess;
            MessageStr = message;
        }
    }
}