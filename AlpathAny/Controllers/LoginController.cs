using Microsoft.AspNetCore.Mvc;

namespace AlpathAny.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
