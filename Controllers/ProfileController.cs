using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using smartmat.Data;

namespace smartmat.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<HomeController> _logger;

            public ProfileController(ILogger<HomeController> logger)
            {
                _logger = logger;
            }

        // GET
        public IActionResult Index()
        {
            return View();
        }
        
        
        
    }
}