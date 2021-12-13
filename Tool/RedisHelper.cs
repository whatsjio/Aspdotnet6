using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Concurrent;

namespace Tool
{
    public class RedisHelper:IDisposable
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private string _connectionString;

        /// <summary>
        /// 实例名称
        /// </summary>
        private string _instanceName;

        //默认数据库
        private int _defaultDB;

        /// <summary>
        /// 系统目录名称
        /// </summary>
        private string _syscustomKey;

        /// <summary>
        ///自定义目录名 
        /// </summary>
        public string CustomKey;

        /// <summary>
        /// 
        /// </summary>
        private ConcurrentDictionary<string, ConnectionMultiplexer> _connections;

        /// <summary>
        /// 是否已执行清理
        /// </summary>
        private bool disposedValue;


        public RedisHelper(string connectionString, string instanceName,string syscustomKey,int defaultDB = 0)
        {
            _connectionString = connectionString;
            _instanceName = instanceName;
            _defaultDB = defaultDB;
            _syscustomKey= syscustomKey;
            _connections = new ConcurrentDictionary<string, ConnectionMultiplexer>();
        }

        /// <summary>
        /// 获取ConnectionMultiplexer
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer GetConnect()
        {
            return _connections.GetOrAdd(_instanceName, p => ConnectionMultiplexer.Connect(_connectionString));
        }

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="db">默认为0：优先代码的db配置，其次config中的配置</param>
        /// <returns></returns>
        public IDatabase GetDatabase()
        {
            return GetConnect().GetDatabase(_defaultDB);
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="endPointsIndex"></param>
        /// <returns></returns>
        public IServer GetServer(int endPointsIndex = 0)
        {
            var confOption = ConfigurationOptions.Parse(_connectionString);
            return GetConnect().GetServer(confOption.EndPoints[endPointsIndex]);
        }

        public ISubscriber GetSubscriber()
        {
            return GetConnect().GetSubscriber();
        }

        #region 获取目录键
        /// <summary>
        /// 获取目录键
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns></returns>
        private string AddSysCustomKey(string key)
        {
            var prefixKey = $"{(string.IsNullOrEmpty(_syscustomKey) ? "" : _syscustomKey + ":")}{(string.IsNullOrEmpty(CustomKey) ? "" : CustomKey + ":")}{key}";
            return prefixKey;
        }
        #endregion


        #region 键值数组转换
        /// <summary>
        /// 键值数组转换
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        private RedisKey[] ConvertRedisKeys(IEnumerable<string> redisKeys)
        {
            return redisKeys.Select(redisKey => (RedisKey)redisKey).ToArray();
        }
        #endregion


        #region 获取多个Key
        /// <summary>
        /// 获取多个Key
        /// </summary>
        /// <param name="listKey"></param>
        /// <returns></returns>
        public RedisValue[] StringGetArry(IEnumerable<string> listKey)
        {
            var newKeys = listKey.Select(AddSysCustomKey);
            return GetDatabase().StringGet(ConvertRedisKeys(newKeys));
        } 
        #endregion

        #region 保存单个key
        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">超时时间(秒) null 不设置时间</param>
        /// <returns></returns>
        public bool StringSet(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            var result = GetDatabase().StringSet(AddSysCustomKey(key), value, expiry);
            return result;
        }
        #endregion

        #region 异步保存单个key
        /// <summary>
        /// 异步保存单个key
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">超时时间(秒) null 不设置时间</param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            var result = GetDatabase().StringSetAsync(AddSysCustomKey(key), value, expiry);
            return await result;
        }
        #endregion

        #region 获取单个key的值
        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        public string StringGet(string key)
        {
            var result = GetDatabase().StringGet(AddSysCustomKey(key));
            return result;
        }
        #endregion

        #region 异步获取单个key的值
        /// <summary>
        /// 异步获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        public async Task<string> StringGetAsync(string key)
        {
            var result = GetDatabase().StringGetAsync(AddSysCustomKey(key));
            return await result;
        }
        #endregion

        #region 获取一个key的对象
        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T StringGet<T>(string key)
        {
            return JsonConvert.DeserializeObject<T>(StringGet(key));
        }
        #endregion

        #region 为数字增字val
        /// <summary>
        /// 为数字增字val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public double StringIncrement(string key, double val = 1)
        {
            return GetDatabase().StringIncrement(AddSysCustomKey(key), val);
        }
        #endregion

        #region 为数字减少val
        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public double StringDecrement(string key, double val = 1)
        {
            return GetDatabase().StringDecrement(AddSysCustomKey(key), val);
        } 
        #endregion


        #region 保存多个键值
        /// <summary>
        /// 保存多个键值
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public bool StringSetList(IEnumerable<KeyValuePair<RedisKey, RedisValue>> keyValues)
        {
            var newkeyValues = keyValues.Select(p => new KeyValuePair<RedisKey, RedisValue>(AddSysCustomKey(p.Key), p.Value)).ToArray();
            return GetDatabase().StringSet(newkeyValues);
        } 
        #endregion


        #region 保存对象
        /// <summary>
        /// 保存对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="obj">值</param>
        /// <param name="expiry">超时时间(秒)</param>
        /// <returns></returns>
        public bool StringSetObject<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            string json = JsonConvert.SerializeObject(obj);
            var result = GetDatabase().StringSet(AddSysCustomKey(key), json, expiry);
            return result;
        } 
        #endregion


        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                }
                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                if (_connections != null && _connections.Count > 0)
                {
                    foreach (var item in _connections.Values)
                    {
                        item.Close();
                    }
                }
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~RedisHelper()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}