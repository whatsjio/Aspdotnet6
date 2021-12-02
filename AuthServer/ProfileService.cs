using IdentityServer4.Models;
using IdentityServer4.Services;

namespace AuthServer
{
    /// <summary>
    /// 用户信息访问
    /// </summary>
    public class ProfileService : IProfileService
    {

        /// <summary>
        /// 构造
        /// </summary>
        public ProfileService()
        {

        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                //depending on the scope accessing the user data.
                var claims = context.Subject.Claims.ToList();

                //set issued claims to return
                context.IssuedClaims = claims.ToList();
            }
            catch (Exception ex)
            {
                //log your error
            }
            await Task.CompletedTask;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            await Task.CompletedTask;
        }
    }
}
