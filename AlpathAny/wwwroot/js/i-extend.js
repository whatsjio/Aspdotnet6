; (function () {
    var iex = {
        notify: {
            success: function (message) {
                Vue.prototype.$Notice.success({
                    title: "提示",
                    desc: message
                })
            },
            error: function (message) {
                Vue.prototype.$Notice.error({
                    title: "提示",
                    desc: message
                })
            },
            info: function (message) {
                Vue.prototype.$Notice.info({
                    title: "提示",
                    desc: message
                })
            },
            warning: function (message) {
                Vue.prototype.$Notice.warning({
                    title: "提示",
                    desc: message
                })
            }
        },
        message: {
            success: function (message) {
                Vue.prototype.$Message.success(message);
            },
            error: function (message) {
                Vue.prototype.$Message.error(message);
            },
            info: function (message) {
                Vue.prototype.$Message.info(message);
            },
            warning: function (message) {
                Vue.prototype.$Message.warning(message);
            }
        },
        confirm: function (message, confirm, cancel) {
            cancel = cancel || function () { };
            Vue.prototype.$Modal.confirm({
                title: "提示",
                content: message,
                onOk: confirm,
                onCancel: cancel
            });
        },
        modal: {
            success: function (message) {
                Vue.prototype.$Modal.success({ title: "提示", content: message });
            },
            error: function (message) {
                Vue.prototype.$Modal.error({ title: "提示", content: message });
            },
            info: function (message) {
                Vue.prototype.$Modal.info({ title: "提示", content: message });
            },
            warning: function (message) {
                Vue.prototype.$Modal.warning({ title: "提示", content: message });
            }
        }
    }
    window.iex = iex;
})();