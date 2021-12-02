using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Newtonsoft.Json;
using System.Security.Claims;

namespace AuthServer
{

    /// <summary>
    /// 用户验证
    /// </summary>
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ResourceOwnerPasswordValidator()
        {

        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (context.UserName == "admin" && context.Password == "123456")
            {
                //成功 后面如果调用userinfo接口只会返回subject内容
                context.Result = new GrantValidationResult(subject: JsonConvert.SerializeObject(new { name = "admin", 
                    time = DateTime.Now,
                    uid = Guid.NewGuid() }), OidcConstants.AuthenticationMethods.Password,claims:GetUserClaims("admin"));
            }
            else
            {
                //验证失败
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "用户信息验证失败");
            }
            //暂时异步处理
            await Task.CompletedTask;
        }


        //可以根据需要设置对应的Claim
        private Claim[] GetUserClaims(string userid)
        {
            return new Claim[]
            {
            new Claim("UserId", userid),
            new Claim(JwtClaimTypes.Name,"wjk"),
            new Claim(JwtClaimTypes.GivenName, "jaycewu"),
            new Claim(JwtClaimTypes.FamilyName, "yyy"),
            new Claim(JwtClaimTypes.Email, "12345666@qq.com"),
            new Claim(JwtClaimTypes.Role,"admin")
            };
        }
    }
}
