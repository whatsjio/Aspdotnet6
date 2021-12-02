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
    }
}
