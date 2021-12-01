
//获取url中的参数
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    if (window.location.href.split('?').length > 1) {
        var r = window.location.href.split('?')[1].match(reg);
        if (r !== null) return unescape(r[2]);
        return null;
    } else {
        return null;
    }
}

//关闭弹出层并刷新父级页面
function refreshParentPage() {
    try {
        window.top.layer.closeAll();
        var id = window.top.v.tagsMenu;
        window.top.$("#iframe" + id)[0].contentWindow.v.currentChange(1);
        window.top.layer.closeAll();
    } catch (e) {
        console.log("这里报错很正常");
        window.parent.v.currentChange(1);
    }
}


//手机号验证
var checkPhone = function (rule, value, callback) {
    if (!value) {
        return callback(new Error('手机号不能为空'));
    } else {
        const reg = /^1[3|4|5|6|7|8][0-9]\d{8}$/
        if (reg.test(value)) {
            callback();
        } else {
            return callback(new Error('请输入正确的手机号'));
        }
    }
};