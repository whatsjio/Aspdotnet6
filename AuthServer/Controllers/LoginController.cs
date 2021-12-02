using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Controllers
{
    public class LoginController : Controller
    {

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }



    }
}
