using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AlpathAny.Controllers
{

    //转发控制
    public class ForwardController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<ForwardController> _logger;

        public ForwardController(IConfiguration configuration, ILogger<ForwardController> logger)
        {
            _configuration = configuration;
            _logger= logger;
        }

        public IActionResult Index()
        {
            //ViewBag.Authorurl = Program.Appraisalurl;
            _logger.LogInformation("haha");
            return View();
        }
    }
}
