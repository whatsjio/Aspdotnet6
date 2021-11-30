using Microsoft.AspNetCore.Mvc;

namespace AlpathAny.Areas.BusinessLogicArea.Controllers
{
    [Area("BusinessLogicArea")]
    public class BusinessController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
