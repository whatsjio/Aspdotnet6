using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Newtonsoft.Json;

namespace AuthServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (context.UserName == "admin" && context.Password == "123456")
            {
                //成功
                context.Result = new GrantValidationResult(subject: JsonConvert.SerializeObject(new { name = "admin", time = DateTime.Now, uid = Guid.NewGuid() }), OidcConstants.AuthenticationMethods.Password);
            }
            else
            {
                //验证失败
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential");
            }
            return Task.CompletedTask;
        }
    }
}
