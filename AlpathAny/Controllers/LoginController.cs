using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlpathAny.Controllers
{
    public class LoginController : Controller
    {
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
        /// 主页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 心跳验证
        /// </summary>
        /// <returns></returns>
        [Authorize]
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
