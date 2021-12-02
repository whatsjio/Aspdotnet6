
/*Vue 菜单 组件*/
Vue.component('side-menu-item', {
    props: ['data'],
    template: `
        <submenu :name="data.name" :key="data.name">

            <template slot="title">
               <Icon v-bind:type="data.icon"></Icon>
               <span>{{data.title}}</span>
            </template>

            <template v-for="item in data.children">
                <template v-if="item.children && item.children.length>0">
                    <side-menu-item  :data="item" :name="item.name" :key="item.name"></side-menu-item>
                </template>
                <menu-item v-else :name="item.name" :key="item.name">
                    <Icon v-bind:type="item.icon"></Icon>
                    <span>{{item.title}}</span>
                </menu-item>

           </template>

        </submenu>
`
});

/* Vue 小菜单 组件*/
Vue.component('collapsed-menu', {
    props: ['data'],
    template: `
    <dropdown transer placement="right-start" @on-click="handleClick">
        <a class="drop-menu-a" type="text">
            <Icon size="20" color="#fff" v-bind:type="data.icon"></Icon>
            <span class="menu-title main_min_menu_title">{{ data.title }}</span>
        </a>
        <dropdown-menu slot="list">
          <template v-for="item in data.children">

              <template v-if="item.children && item.children.length>0">
                   <collapsed-menu v-bind:data="item"></collapsed-menu>
              </template>

             <dropdown-item v-else  v-bind:name="item.name">
                <Icon size="16" :type="item.icon"></Icon>
                <span class="menu-title main_min_menu_title">{{ item.title }}</span>
            </dropdown-item>

          </template>
        </dropdown-menu>
    </dropdown>
`, methods: {
        handleClick(name) {
            this.$emit('on-click', name)
        }
    }

});

//页面状态
Vue.component("page-admingroup", {
    props: ['state'],
    template: `
    <span :style="'color:'+color">{{txt}}</span>
`, data: function () {
        return {
            color: "",
            txt: ""
        }
    },
    mounted: function () {
        if (this.state) {
            this.color = "#19be6b";
            this.txt = "启用";
        } else {
            this.color = "#ccc";
            this.txt = "禁用";
        }
    }
});

//上传组件

/**
1、地址
2、类型
3、返回值
4、大小
5、最多个数

**/
Vue.component("page-upload", {
    props: ['data'],
    template: `
<div> 
    <div class="demo-upload-list" v-for="item in uploadList">
        <img :src="item.url">
        <div class="demo-upload-list-cover">
            <Icon type="ios-eye-outline" @click.native="handleView(item.url)"></Icon>
            <Icon type="ios-trash-outline" @click.native="handleRemove(item)"></Icon>
        </div>
    </div>
<Upload
    ref="upload"
    :show-upload-list="false"
    :default-file-list="uploadList"
    :on-success="handleSuccess"
    :format="format"
    :max-size="size"
    :on-format-error="handleFormatError"
    :on-exceeded-size="handleMaxSize"
    :before-upload="handleBeforeUpload"
    :multiple="multiple"
    type="drag"
    :action="action"
    style="display: inline-block;width:58px;">
    <div style="width: 58px;height:58px;line-height: 58px;">
        <Icon type="ios-camera" size="20"></Icon>
    </div>
</Upload>
<modal title="图片预览" v-model="visible">
    <img :src="imgName" v-if="visible" style="width: 100%">
</modal>

</div>
`, data: function () {
        return {
            uploadList: [],
            visible: false,
            format: ['jpg', 'jpeg', 'png'],
            size: 2048,
            action: "../Home/UploadFile",
            imgName: "",
            max: 0,
            multiple: false
        }
    },
    mounted: function () {
        if (this.data.multiple) {
            this.multiple = this.data.multiple;
        }
        if (this.data.max) {
            this.max = this.data.max;
        }
        if (this.data.imgList.length > 0) {
            this.uploadList = this.data.imgList;
        }
    },
    methods: {
        handleView: function (name) {
            this.imgName = name;
            this.visible = true;
        },
        handleRemove: function (file) {
            this.uploadList = this.uploadList.filter(item => { return item != file });
        },
        handleSuccess: function (res, file) {
            iview.LoadingBar.finish();
            if (!res.flag) return iview.notice.error(res.msg);
            this.uploadList.push({
                name: res.msg.name,
                url: res.msg.url
            });
            this.$emit('changeimg', this.uploadList);
        },
        handleFormatError: function (file) {
            iview.notice.error("文件不正确：" + file.name);
        },
        handleMaxSize: function (file) {
            iview.notice.error('文件 ' + file.name + ' 太大，不能超过 ' + this.size + 'KB。');
        },
        handleBeforeUpload: function () {
            if (this.max != 0) {
                if (this.uploadList.length >= this.max) {
                    iview.notice.error("文件最多只能上传" + this.max + "个");
                    return false;
                }
            }
            iview.LoadingBar.start();
        },
    },
    watch: {
        'data.imgList': function () {
            if (this.data.imgList.length > 0) {
                this.uploadList = this.data.imgList;
            }
        }
    }
});


/*部门组件*/
Vue.component('department-menu', {
    props: ['data'],
    template: `
<ul class="d_menu_ul">
    <li class="d_menu_ul_li" @click.stop="deptMenuClick('add')">添加子部门</li>
    <li class="d_menu_ul_li" @click.stop="deptMenuClick('edit')">修改名称</li>
    <li class="d_menu_ul_li" @click.stop="deptMenuClick('delete')">删除</li>
    <li>部门ID：{{data.id}}</li>
</ul>
`,
    data: function () {
        return {}
    },
    mounted: function () {

    },
    methods: {
        deptMenuClick: function (v) {
            console.log(v);
            this.$emit('deptmenuclick', v);
        }
    }
});

/*百度编辑器*/
Vue.component('i-ueditor', {
    template: `<script id="editor" type="text/plain"></script>`,
    props:["data"],
    data: function () {
        return {
            editor: null,
            txt: "",
            config: {}
        }
    },
    mounted() {
        const _this = this;
        this.editor = UE.getEditor('editor', this.data.config); // 初始化UE
        this.editor.addListener("ready", function () {
            console.log(_this.data.txt);
            _this.editor.setContent(_this.data.txt); // 确保UE加载完成后，放入内容。
        });
    },
    methods: {
        getUEContent() { // 获取内容方法
            return this.editor.getContent();
        }
    },
    destroyed() {
        this.editor.destroy();
    }

});

/*图片显示标签*/
Vue.component('i-img', {
    props: ["src"],
    template: `<a :href='src' target="_blank"><Avatar :src="src"></Avatar></a>`,
    data: function () {
        return {
            src: ""
        }
    },
    mounted() {
    }
});