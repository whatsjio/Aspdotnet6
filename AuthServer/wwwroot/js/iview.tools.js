

var iview = {
    //通知提醒
    notice: {
        info: function (title, msg) {
            window.top.Vue.prototype.$Notice.info({ title: title, desc: msg });
        },
        success: function (title, msg) {
            window.top.Vue.prototype.$Notice.success({ title: title, desc: msg });
        },
        warning: function (title, msg) {
            window.top.Vue.prototype.$Notice.warning({ title: title, desc: msg });
        },
        error: function (title, msg) {
            window.top.Vue.prototype.$Notice.error({ title: title, desc: msg });
        }
    },
    //消息
    message: {
        success: function (msg) {
            window.top.Vue.prototype.$Message.success(msg);
        },
        error: function (msg) {
            window.top.Vue.prototype.$Message.error(msg);
        },
        info: function (msg) {
            window.top.Vue.prototype.$Message.info(msg);
        },
        warning: function (msg) {
            window.top.Vue.prototype.$Message.warning(msg);
        },
        loading: function (msg) {
            window.top.Vue.prototype.$Message.loading(msg);
        }
    },
    /*对话框*/
    confirm: function (title, message, confirm, cancel) {
        cancel = cancel || function () { };
        window.top.Vue.prototype.$Modal.confirm({
            title: title,
            content: message,
            onOk: confirm,
            onCancel: cancel
        });
    },
    //模态框
    modal: {
        success: function (msg) {
            window.top.Vue.prototype.$Modal.success({ title: "提示", content: msg });
        },
        error: function (msg) {
            window.top.Vue.prototype.$Modal.error({ title: "提示", content: msg });
        },
        info: function (msg) {
            window.top.Vue.prototype.$Modal.info({ title: "提示", content: msg });
        },
        warning: function (msg) {
            window.top.Vue.prototype.$Modal.warning({ title: "提示", content: msg });
        }
    },
    //加载
    load: {
        show: function (t) {
            var str = "Loading";
            if (t) { str = t; }
            window.top.Vue.prototype.$Spin.show({
                render: (h) => {
                    return h('div', [
                        h('Icon', {
                            'class': 'demo-spin-icon-load',
                            props: {
                                type: 'ios-loading',
                                size: 18
                            }
                        }),
                        h('div', str)
                    ])
                }
            });
        },
        show1: function () {
            window.top.Vue.prototype.$Spin.show();
        },
        hide: function () {
            window.top.Vue.prototype.$Spin.hide();
        }
    },
    //加载进度条
    LoadingBar: {
        start: function () {//开始
            window.top.Vue.prototype.$Loading.start();
        },
        finish: function () {//结束
            window.top.Vue.prototype.$Loading.finish();
        },
        error: function () {//报错
            window.top.Vue.prototype.$Loading.error();
        }
    }
};
