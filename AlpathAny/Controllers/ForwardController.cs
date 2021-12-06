using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AlpathAny.Controllers
{

    //转发控制
    public class ForwardController : Controller
    {
        private readonly IConfiguration _configuration;

        public ForwardController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            //ViewBag.Authorurl = Program.Appraisalurl;
            return View();
        }
    }
}
