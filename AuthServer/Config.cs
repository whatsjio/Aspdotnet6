using IdentityServer4;
using IdentityServer4.Models;
using static IdentityServer4.IdentityServerConstants;

namespace AuthServer
{
    /// <summary>
    /// 认证配置信息
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// api资源配置
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> { new ApiResource("api", "我的第一个API") { Scopes = { "api" } } };
        }

        /// <summary>
        /// 定义资源范围
        /// </summary>
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new ApiScope[]
            {
               new ApiScope("api")
            };
        }

        /// <summary>
        /// 定义访问的资源客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
        {
            new Client{
                ClientId="client",//定义客户端ID
                ClientSecrets=
                {
                    new Secret("secret".Sha256())//定义客户端秘钥
                },
                AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,//授权方式为用户密码模式授权，类型可参考GrantTypes枚举
                AllowOfflineAccess=true,//需要refresh_tokens 刷新token
                AccessTokenLifetime=60*5, //设置token过期时间
                /*Absolute-即RefreshToken的过期策略采用绝对过期时间，即到了AbsoluteRefreshTokenLifetime设置的值后就直接失效
                Sliding-过期策略采用滑动过期时间，即每次使用这个RefreshToken刷新令牌时，就会重新设置这个RefreshToken的
                过期时间 = 原本到期时间+滑动过期时间(SlidingRefreshTokenLifetime)但这个时间不会超过
                AbsoluteRefreshTokenLifetime的值(除非AbsoluteRefreshTokenLifetime的值为0)。*/
                RefreshTokenExpiration=TokenExpiration.Sliding,
                SlidingRefreshTokenLifetime=60*15, //设置RefreshToken的滑动过期时间
                AbsoluteRefreshTokenLifetime=0, //设置RefreshToken的绝对过期时间
                RefreshTokenUsage=TokenUsage.OneTimeOnly,//RefreshToken可以反复使用 OneTime-只能使用一次。刷新时会颁发新的RefreshToken
                UpdateAccessTokenClaimsOnRefresh=true,  //是否在使用RefreshToken刷新AccessToken的时候，是否返回一个新的AccessToken并更新其声明(Claims)
                AllowedScopes={
                    "api",
                    StandardScopes.OpenId,
                    StandardScopes.OfflineAccess //如果要获取refresh_tokens ,必须在scopes中加上OfflineAccess
                }//允许客户端访问的范围

            }
       };
        }


        /// <summary>
        /// 这个方法是来规范tooken生成的规则和方法的。一般不进行设置，直接采用默认的即可。
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            };
        }


    }
}
