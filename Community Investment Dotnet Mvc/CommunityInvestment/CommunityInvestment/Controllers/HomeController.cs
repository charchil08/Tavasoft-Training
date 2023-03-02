using CommunityInvestment.Entities.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommunityInvestment.Controllers
{
    public class HomeController : Controller
    {
        private readonly CommunityInvestmentContext _context;
        private readonly IConfiguration _configuration;

        public HomeController(CommunityInvestmentContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            IEnumerable<SelectListItem> CountryList = _context.Countries.ToList().Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.CountryId.ToString()
                    }
                );
            ViewBag.CountryList = CountryList; 
            return View("_SearchFilterHeader");
        }

        public IActionResult MissionDetail()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}