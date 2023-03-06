using CommunityInvestment.Entities.Data;
using CommunityInvestment.Entities.DataModels;
using CommunityInvestment.Models.ViewModels;
using CommunityInvestment.Models.ViewModels.Mission;
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

        public IActionResult FetchCityBasedOnCountry([FromQuery] string countryId)
        {
            IEnumerable<City> cities = _context.Cities.Where(c => c.CountryId == (long)Convert.ToDouble(countryId)).ToList();
            SearchFilterHeaderModel searchFilterHeaderModel = new();
            searchFilterHeaderModel.CountryId = (long)Convert.ToDouble(countryId);
            searchFilterHeaderModel.CityList = cities;

            ViewBag.SelectedCityList = cities.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CityId.ToString()
            });

            return PartialView("_CityFilterHeader", searchFilterHeaderModel);
        }

        public IActionResult GetAllMissions()
        {
            IEnumerable<MissionCard> missions = (from m in _context.Missions
                                                             join c in _context.Cities on m.CityId equals c.CityId
                                                             join md in _context.MissionDocuments on m.MissionId equals md.MissionId
                                                             join mt in _context.MissionTypes on m.MissionTypeId equals mt.MissionTypeId
                                                             join mth in _context.MissionThemes on m.MissionThemeId equals mth.MissionThemeId
                                                             select new MissionCard()
                                                             {
                                                                 MissionId = m.MissionId,
                                                                 Title = m.Title,
                                                                 ShortDescription = m.ShortDescription,
                                                                 Description = m.Description,
                                                                 CityId = c.CityId,
                                                                 CityName = c.Name,
                                                                 DocumentName = md.DocumentName,
                                                                 DocumentPath = md.DocumentPath,
                                                                 MissionTypeId = mt.MissionTypeId,
                                                                 MissionTypeName = mt.Name,
                                                                 MissionThemeId = mth.MissionThemeId,
                                                                 MissionThemeName = mth.Title
                                                             }).ToList();

            return PartialView("_MissionCardsGrid", missions);

        }
    }
}