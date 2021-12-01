//全局配置
//layer.config({ extend: 'skin/moon/style.css' });
//http://layer.layui.com/api.html#layer.open

/*
0、感叹号
1、正确
2、错误
3、疑问
4、锁
5、不高兴
6、小脸
*/
var LayerTools = {
    msg: function (m, i) {
        layer.msg(m, { icon: i });
    },
    alert: function (m, i, c) {
        layer.alert(m, {
            icon: i, //i:0-6图标
            closeBtn: 1, //种类1和2,0代表不显示
            shade: 0.3, //遮罩[0.8, '#393D49']
            shadeClose: false, //是否点击遮罩关闭
            time: 0, //代表自动关闭时间
            shift: 0//动画0-6
        }, c); //c:回调函数
    },
    load: function (icon) {
        return layer.load(icon); //0-2
    },
    close: function (index) {
        layer.close(index); //对象
    },
    closeAll: function (index) {
        layer.closeAll(index);
    },
    confirm: function (msg, icon, title, callback) {
        layer.confirm(msg, { icon: icon, title: title }, callback);
    },
    tips: function (msg, dom, tips) {
        layer.tips(msg, dom, {//dom:'#id'
            tips: tips// 方向和颜色tips: [1, '#c00']
        });
    },
    prompt: function (type, msg) {//未完成 用到在修改
         layer.prompt({
            formType: type,
            value: msg, //初始值
            title: title
        }, function (value, index, elem) {
            // alert(value); //得到value
            // layer.close(index);
        });
    },
    openpage: function (title, width, height, url) {
        return window.top.layer.open({
            type: 2,
            title: title,
            content: url,
            area: [width, height]
        });
    }
};