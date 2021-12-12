global using MiddlewareService.Iservice;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Tool;

namespace AlpathAny.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private readonly IVerificationService _verificationService;
        private readonly IDatabase _redisService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="verificationService"></param>
        public LoginController(IVerificationService verificationService, RedisHelper redisHelper)
        {
            _verificationService = verificationService;
            _redisService= redisHelper.GetDatabase();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get() {
            var a= new JsonResult(from c in HttpContext.User.Claims select new { c.Type, c.Value });
            var result=await Task.FromResult(a);
            return result;
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResharperToken(string token) {
            var result = await _verificationService.RefreshToken(token);
            return new JsonResult(result);
        }


        /// <summary>
        /// 心跳验证
        /// </summary>
        /// <returns></returns>
        
        [HttpGet]
        public async Task<IActionResult> Heartbeat()
        {
            var resultmodel = new
            {
                success = true,
                Message="成功"
            };
            var result = await Task.FromResult(new JsonResult(resultmodel));
            return result;
        }

        /// <summary>
        /// 登出测试
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> LogOut() {
            await HttpContext.SignOutAsync();
            var a = new {
                su="3213"
            };
            return new JsonResult(a);
        }

    }
}
