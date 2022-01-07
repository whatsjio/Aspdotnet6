namespace MiddlewareService.Iservice
{
    /// <summary>
    /// 登录接口
    /// </summary>
    public interface ILoginService: IBaseService
    {
        /// <summary>
        /// 初始化管理组
        /// </summary>
        Task InitializeAdmin();
    }
}
