using CommunityInvestment.Entities.Data;
using CommunityInvestment.Entities.DataModels;
using CommunityInvestment.Models.ViewModels;
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
            SearchFilterHeaderModel sfm = new();
            sfm.CountryList = CountryList;
            return View(sfm);
        }

        public IActionResult MissionDetail()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public JsonResult FetchCityBasedOnCountry([FromQuery]string countryId)
        {
            List<City> cities = _context.Cities.Where(c => c.CountryId == (long)Convert.ToDouble(countryId)).ToList();
            return Json(cities);
        }
    }
}