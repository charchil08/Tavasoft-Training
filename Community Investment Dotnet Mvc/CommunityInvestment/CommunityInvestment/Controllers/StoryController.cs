using Microsoft.AspNetCore.Mvc;

namespace CommunityInvestment.Controllers
{
    public class StoryController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public StoryController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}