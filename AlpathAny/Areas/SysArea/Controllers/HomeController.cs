using Microsoft.AspNetCore.Mvc;

namespace AlpathAny.Areas.SysArea.Controllers
{
    [Area("SysArea")]
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

    }
}
