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
            IEnumerable<MissionTheme> missionThemes = _context.MissionThemes.ToList();

            IEnumerable<Skill> skills = _context.Skills.ToList();

            SearchFilterHeaderModel sfm = new();
            sfm.CountryList = CountryList;
            sfm.MissionThemesList = missionThemes;
            sfm.SkillsList = skills;
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

        public IActionResult FetchCityBasedOnCountry([FromQuery]string countryId)
        {
            IEnumerable<City> cities = _context.Cities.Where(c => c.CountryId == (long)Convert.ToDouble(countryId)).ToList();
            SearchFilterHeaderModel searchFilterHeaderModel = new();
            searchFilterHeaderModel.countryId = (long)Convert.ToDouble(countryId);
            searchFilterHeaderModel.CityList = cities;

            ViewBag.SelectedCityList = cities.Select(c => new SelectListItem
            {
                Text=c.Name,
                Value=c.CityId.ToString()
            });

            return PartialView("_CityFilterHeader", searchFilterHeaderModel);
        }
    }
}