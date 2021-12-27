const getBaseUrl = (env) => {
    let getkey = env || 'production';
    let base = {
        production: 'http://localhost:5025',
        development: 'http://localhost:5025',
        test: 'http://localhost:5025',
    }[getkey];
    if (!base) {
        base = '/';
    }
    return base;
};

//封装类型
class AxiosData {
    //构造函数
    constructor(issucess, messagestr, data) {
        this.IsSucess = issucess;
        this.MessageStr = messagestr;
        this.Data = data;
    }
}


class NewAxios {
    //构造
    constructor() {
        //设置请求地址配置
        this.baseURL = getBaseUrl();
        //设置超时10秒
        this.timeout = 10000;
        //跨域请求时是否需要使用凭证
        this.withCredentials = false;
    }
    // 这里的url可供你针对需要特殊处理的接口路径设置不同拦截器。
    //setverify 是否添加验证  endverify 是否终止验证请求
    setInterceptors = (instance, url, setverify = true, endverify = false) => {
        //请求拦截器
        instance.interceptors.request.use((config) => {
            // 在这里添加loading
            // 配置token
            if (setverify) {
                let gettoken = GetToken();
                config.headers['Authorization'] = 'Bearer ' + gettoken.access_token;
            }
            //默认返回json格式
            config.responseType = 'json';
            if (config.headers['Content-Type'] && config.headers['Content-Type'] == 'application/x-www-form-urlencoded') {
                config.data = Qs.stringify(config.data);
            }
            //localStorage.getItem('AuthorizationToken') || '';
            return config;
        }, err => Promise.resolve(new AxiosData(false, "请求配置异常", err)));


        //响应拦截器
        instance.interceptors.response.use((response) => {
            // 在这里移除loading
            // todo: 想根据业务需要，对响应结果预先处理的，都放在这里
            return new AxiosData(response.data.IsSucess, response.data.messageStr, response.data.Data);
        }, (err) => {
            let responserr = new AxiosData(false, `请求结果异常:响应代码:${err.response.status}`, err.response);
            // 响应错误码处理
            if (err.response) { 
                if(err.response.status===401&&!endverify){
                    let verifyresult = Continuerequest(err.response.config);
                    return Promise.resolve(verifyresult);
                }
                else if(err.response.status===403){

                }
                else if(err.response.status===400){

                }
                else if(err.response.status===500){

                }
                return Promise.resolve(responserr);
            }
            if (!window.navigator.online) { // 断网处理
                // todo: jump to offline page
                return Promise.resolve(new AxiosData(false, `网络已断开`, err.response));
            }
            return Promise.resolve(responserr);
        });

    }

    //setverify 是否带验证，endverify，endverify是否终止验证校验
    request(options, setverify = true, endverify = false) {
        // 每次请求都会创建新的axios实例。
        const instance = axios.create();
        const config = { // 将用户传过来的参数与公共配置合并。
            ...options,
            baseURL: this.baseURL,
            timeout: this.timeout,
            withCredentials: this.withCredentials,
        };
        // 配置拦截器，支持根据不同url配置不同的拦截器。
        this.setInterceptors(instance, options.url, setverify, endverify);
        return instance(config);
    }

    //继续验证请求
    Continuerequest(axiosconfig) {
        RefreshToken().then(success => {
            request(axiosconfig, true, true).then(contiunecs => {
                return contiunecs;
            }).catch(err => {
                return err;
            });
        })
    }

    //刷新token
    RefreshToken() {
        let refreshpromise = new Promise((resolve, reject) => {
            let gettoken = GetToken();
            let requestuser = gettoken.username;
            let requesttoken = gettoken.access_token;
            let configs = {
                method: 'get',
                url: '/Login/ResharperToken',
                params: {
                    token: requesttoken,
                    username: requestuser
                }
            };
            this.request(configs, false, true).then(success => {
                //token刷新成功
                if (success.IsSucess) {
                    //重设用户凭证
                    localStorage.setItem(this.form.userName, JSON.stringify(success.Data));
                    localStorage.setItem('keyname', requestuser);
                    resolve("刷新成功")
                }
                else {
                    console.log(success.MessageStr)
                    tologin();
                }
            }, failed => {
                tologin();
            });
        })
        return refreshpromise;
    }
}

(function (wd) {
    wd.httpaxios = new NewAxios();
})(window)