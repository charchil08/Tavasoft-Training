using CommunityInvestment.Entities.Data;
using CommunityInvestment.Entities.DataModels;
using CommunityInvestment.Models.ViewModels;
using CommunityInvestment.Models.ViewModels.Mission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using CommunityInvestment.SP;

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
            IEnumerable<Country> countries = _context.Countries.ToList();
            IEnumerable<MissionTheme> missionThemes = _context.MissionThemes.ToList();
            IEnumerable<Skill> skills = _context.Skills.ToList();

            Common2 c = new Common2();
            c.Test();

            SearchFilterHeaderModel sfm = new();
            sfm.CountryList = countries;
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

        public IActionResult FetchCityBasedOnCountry([FromBody] List<long> Countries)
        {
            IEnumerable<City> cities = (List<City>)(from city in _context.Cities where Countries.Contains((long)city.CountryId) select city).ToList();
            SearchFilterHeaderModel searchFilterHeaderModel = new();
            searchFilterHeaderModel.CityList = cities;
            
            ViewBag.SelectedCityList = cities.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CityId.ToString()
            });

            return PartialView("_CityFilterHeader", searchFilterHeaderModel);
        }

        public IActionResult GetAllMissions([FromBody] Filters filters)
        {

            List<MissionCard> missions = (from m in _context.Missions
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
                                          })
                                      .Where(m => m.Title.ToLower().Contains(filters.SearchKeyword.ToLower())).ToList();

            if (filters.Cities != null && filters.Cities.Any())
            {
                missions = (List<MissionCard>)(from mission in missions where filters.Cities.Contains(mission.CityId) select mission).ToList();
            }

            if (filters.Themes != null && filters.Themes.Any())
            {
                missions = (List<MissionCard>)(from mission in missions where filters.Themes.Contains(mission.MissionThemeId) select mission).ToList();
            }

            if (filters.Skills != null && filters.Skills.Any())
            {
                missions = (List<MissionCard>)(from mission in missions where filters.Skills.Contains(mission.MissionThemeId) select mission).ToList();
            }

            return PartialView("_MissionCardsGrid", missions);

        }

        public int Execute(string commandText, object? param = null, SqlTransaction? transaction = null, int? commandTimeout = null, bool isSP = true)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("CiConnection")))
            {
                if (isSP)
                {
                    return db.Execute(commandText, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: CommandType.StoredProcedure);
                }
                else
                {
                    return db.Execute(commandText, param);
                }
            }
        }
    }
}