(function ($) {
    $.request = function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        if (window.location.href.split('?').length > 1) {
            var r = window.location.href.split('?')[1].match(reg);
            if (r != null) return unescape(r[2]);
            return "";
        } else {
            return "";
        }
    }

    $.showLoading = function () {
        $("#wechat-loading").show();
    }

    $.hideLoading = function () {
        $("#wechat-loading").hide();
    }

    $.newGuid = function () {
        var S4 = function () {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
        };
        return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
    }

    $.scrollBottom = function (callback) {
        document.body.onscroll = function () {
            var scrollTop = this.scrollTop;
            var scrollHeight = this.scrollHeight;
            var windowHeight = window.innerHeight;
            if (scrollTop + windowHeight == scrollHeight) {
                callback();
            }
        }
    }

})(jQuery);
