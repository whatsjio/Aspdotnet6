
using PlatData.SysTable;
using Tool.SecretHelp;

namespace MiddlewareService.Service
{
    public class LoginService : BaseService, ILoginService
    {
        private readonly ISysAdmin _isysadmin;
        private readonly ISysAdminGroup _isysAdminGroup;
        private readonly ISysAdminMenu _isysAdminMenu;
        private readonly ISysMenu _isysMenu;
        private readonly RedisHelper _redishelp;

        /// <summary>
        /// DI
        /// </summary>
        /// <param name="sysAdmin"></param>
        public LoginService(ISysAdmin sysAdmin, ISysAdminGroup sysAdminGroup, ISysAdminMenu sysAdminMenu, ISysMenu sysMenu, RedisHelper redisHelper)
        {
            _isysadmin=sysAdmin;
            _isysAdminGroup = sysAdminGroup;
            _isysAdminMenu = sysAdminMenu;
            _isysMenu = sysMenu;
            _redishelp = redisHelper;
        }


        public void Login(string userName,string password) {
            //_redishelp.g

        }

        /// <summary>
        /// 初始化管理组
        /// </summary>
        public async Task InitializeAdmin() {
            var getadmingroup = _isysAdminGroup.FirstNoTrack(t => t.Name == "超级管理员");
            if (getadmingroup == null) {
                var addgroup = new SysAdminGroup()
                {
                    Name = "超级管理员",
                    Sort = 1
                };
                var adminmodel= new SysAdmin();
                adminmodel.UserName = "Admin";
                adminmodel.Sex = "男";
                adminmodel.Avatar = "https://ss1.bdstatic.com/70cFvXSh_Q1YnxGkpoWK1HF6hhy/it/u=3448484253,3685836170&fm=27&gp=0.jpg";
                adminmodel.NickName = "超级管理员";
                adminmodel.Salt= HashHelp.GetPbkdf2Salt();
                adminmodel.PassWord = HashHelp.GetPbkdf2("123456", adminmodel.Salt);
                //var creatadmin= await _isysadmin.CreateNew(adminmodel);
                //var creatadminsave= await _isysadmin.SaveTrackAsync();
                addgroup.SysAdminList.Add(adminmodel);
                //adminmodel.Parent = addgroup;
                addgroup.MenuList = new SysAdminMenu()
                {
                    ByGroup = addgroup
                };
                SysMenu Menu = new SysMenu()
                {
                    Title = "系统设置",
                    Icon = "ios-settings-outline",
                    Name = "sys_setting",
                    Sort = 1,
                    isDel = true
                };
                Menu.ChildrenList.Add(new SysMenu
                {
                    Title = "管理组设置",
                    Url = "../SystemSetup/AdminGroupList",
                    Icon = "md-options",
                    Name = "sys_setting_usergroup",
                    Sort = 1,
                    isDel = true
                });
                Menu.ChildrenList.Add(new SysMenu
                {
                    Title = "用户管理",
                    Url = "../SystemSetup/AdminList",
                    Icon = "ios-person-outline",
                    Sort = 2,
                    isDel = true,
                    Name = "sys_setting_user"
                });
                Menu.ChildrenList.Add(new SysMenu
                {
                    Title = "菜单管理",
                    Url = "../SystemSetup/MenuList",
                    Icon = "ios-person-outline",
                    Sort = 3,
                    isDel = true,
                    Name = "sys_setting_menu"
                });
                addgroup.MenuList.Menus.Add(Menu);
                var message= await _isysAdminGroup.CreateNew(addgroup);
                var saveresult = await _isysMenu.SaveTrackAsync();
            }
           
        }
    }
}
