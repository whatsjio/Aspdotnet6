const getBaseUrl = (env) => {
    let base = {
        production: 'http://localhost:5006',
        development: 'http://localhost:5006',
        test: 'http://localhost:5006',
    }[env];
    if (!base) {
        base = '/';
    }
    return base;
};

class NewAxios {
    //构造
    constructor() {
        //设置请求地址配置
        this.baseURL = getBaseUrl('development');
        //设置超时10秒
        this.timeout = 10000;
        //跨域请求时是否需要使用凭证
        this.withCredentials = false;
    }
    // 这里的url可供你针对需要特殊处理的接口路径设置不同拦截器。
    setInterceptors = (instance, url) => {
        instance.interceptors.request.use((config) => {
            // 在这里添加loading
            // 配置token
            config.headers.AuthorizationToken = 'tests';
            //默认返回json格式
            config.responseType = 'json';
            if (config.headers['Content-Type'] && config.headers['Content-Type'] =='application/x-www-form-urlencoded') {
                config.data = Qs.stringify(config.data);
            }
            //localStorage.getItem('AuthorizationToken') || '';
            return config;
        }, err => Promise.reject(err));

        instance.interceptors.response.use((response) => {
            // 在这里移除loading
            // todo: 想根据业务需要，对响应结果预先处理的，都放在这里
            return response;
        }, (err) => {
            if (err.response) { // 响应错误码处理
                switch (err.response.status) {
                    case 403:
                        // todo: handler server forbidden error
                        break;
                    case 400:
                        break;
                    // todo: handler other status code
                    default:
                        break;
                }
                return Promise.reject(err.response);
            }
            if (!window.navigator.online) { // 断网处理
                // todo: jump to offline page
                console.log('异常：连接断开');
            }
            return Promise.reject(err);
        });
    }
    request(options) {
        // 每次请求都会创建新的axios实例。
        const instance = axios.create();
        const config = { // 将用户传过来的参数与公共配置合并。
            ...options,
            baseURL: this.baseURL,
            timeout: this.timeout,
            withCredentials: this.withCredentials,
        };
        // 配置拦截器，支持根据不同url配置不同的拦截器。
        this.setInterceptors(instance, options.url);
        return instance(config);

    }
}

(function (wd) {
    wd.httpaxios =new NewAxios();
})(window)