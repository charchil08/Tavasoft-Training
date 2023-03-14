using CommunityInvestment.Entities.Data;
using CommunityInvestment.Entities.DataModels;
using CommunityInvestment.Models.ViewModels;
using CommunityInvestment.Models.ViewModels.Mission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using CommunityInvestment.Entities.StoredProcModels;

namespace CommunityInvestment.Controllers
{
    public class HomeController : Controller
    {
        private readonly CommunityInvestmentContext _context;
        private static readonly IConfiguration _configuration = new ConfigurationBuilder()
                                                                .SetBasePath(Directory.GetCurrentDirectory())
                                                                .AddJsonFile("appsettings.json")
                                                                .Build();
        private static readonly string _connectionString = _configuration.GetConnectionString("CiConnection");

        public HomeController(CommunityInvestmentContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Country> countries = _context.Countries.ToList();
            IEnumerable<MissionTheme> missionThemes = _context.MissionThemes.ToList();
            IEnumerable<Skill> skills = _context.Skills.ToList();

            SearchFilterHeaderModel sfm = new();
            sfm.CountryList = countries;
            sfm.MissionThemesList = missionThemes;
            sfm.SkillsList = skills;
            return View(sfm);
        }

        [HttpGet]
        //QueryString - MissionId
        //https://localhost:7012/Home/MissionDetail/?MissionId=2
        public IActionResult MissionDetail([FromQuery] long MissionId)
        {
            SpMissionDetail missionDetail = new();
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new("spGetMissionDetail", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 120;
                    using (SqlDataAdapter adapter = new(cmd))
                    {

                        cmd.Parameters.AddWithValue("@MissionId", MissionId);
                        try
                        {
                            DataSet ds = new();
                            adapter.Fill(ds);
                           
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                missionDetail.MissionId = (long)ds.Tables[0].Rows[0]["MissionId"];
                                missionDetail.Title = (string)ds.Tables[0].Rows[0]["Title"];
                                missionDetail.ShortDesc = (string)ds.Tables[0].Rows[0]["ShortDesc"];
                                missionDetail.Description = (string)ds.Tables[0].Rows[0]["Description"];
                                
                                missionDetail.StartDate = ds.Tables[0].Rows[0]["StartDate"]!= System.DBNull.Value ? (DateTime)ds.Tables[0].Rows[0]["StartDate"]: null;
                                missionDetail.EndDate = ds.Tables[0].Rows[0]["EndDate"]!= DBNull.Value ? (DateTime)ds.Tables[0].Rows[0]["EndDate"]: null;
                                missionDetail.EnrolledUser = ds.Tables[0].Rows[0]["EnrolledUser"] != System.DBNull.Value ? (int)ds.Tables[0].Rows[0]["EnrolledUser"] : null;
                                missionDetail.Deadline = ds.Tables[0].Rows[0]["Deadline"]!= System.DBNull.Value ? (DateTime)ds.Tables[0].Rows[0]["Deadline"]: null;

                                missionDetail.GoalObjectiveText = ds.Tables[0].Rows[0]["GoalObjectiveText"]!= System.DBNull.Value ? (string)ds.Tables[0].Rows[0]["GoalObjectiveText"]: null;
                                missionDetail.SeatsLeft = ds.Tables[0].Rows[0]["SeatsLeft"] != System.DBNull.Value ? (int)ds.Tables[0].Rows[0]["SeatsLeft"] : null;
                                missionDetail.AchievedGoalValue = ds.Tables[0].Rows[0]["AchievedGoalValue"]!= System.DBNull.Value ? (int)ds.Tables[0].Rows[0]["AchievedGoalValue"]: null;
                                
                                missionDetail.CityId = (long)ds.Tables[0].Rows[0]["CityId"];
                                missionDetail.CityName = (string)ds.Tables[0].Rows[0]["CityName"];
                                missionDetail.ThemeId = (long)ds.Tables[0].Rows[0]["ThemeId"];
                                missionDetail.ThemeName = (string)ds.Tables[0].Rows[0]["ThemeName"];
                                missionDetail.OrganizationName = (string)ds.Tables[0].Rows[0]["OrganizationName"];
                                missionDetail.SkillName = ds.Tables[0].Rows[0]["SkillName"] != System.DBNull.Value ? (string?)ds.Tables[0].Rows[0]["SkillName"] : null; 
                                missionDetail.AvailabilityId = (byte)ds.Tables[0].Rows[0]["AvailabilityId"];
                                missionDetail.AvailibilityDays = (string)ds.Tables[0].Rows[0]["AvailibilityDays"];
                                missionDetail.Rating = (decimal)ds.Tables[0].Rows[0]["Rating"];

                                missionDetail.IsFavouriteMission= Convert.ToBoolean(ds.Tables[0].Rows[0]["IsFavouriteMission"]);

                            }
                            connection.Close();
                            return View(missionDetail);

                        }
                        catch (Exception ex)
                        {
                            connection.Close();
                        }

                    }
                }

            }
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
            var searchKeyword = filters.SearchKeyword;
            var countries = string.Join(",", filters.Countries);
            var cities = string.Join(",", filters.Cities);
            var themes = string.Join(",", filters.Themes);
            var skills = string.Join(",", filters.Skills);
            var sortColumn = filters.SortColumn;
            var sortOrder = filters.SortOrder;
            var pageIndex = filters.PageIndex;
            var pageSize = filters.PageSize;
            List<SpGetAllMissions> missionCards = _context.GetAllMissions.FromSqlInterpolated($@"
                EXECUTE dbo.spGetAllMissions
                    @SearchKeyword = {searchKeyword},
                    @Countries = {countries},
                    @Cities = {cities},
                    @Themes = {themes},
                    @Skills = {skills},
                    @SortColumn = {sortColumn},
                    @SortOrder = {sortOrder},
                    @PageIndex = {pageIndex},
                    @PageSize = {pageSize}")
                .ToList();

            #region linq query
            //List<MissionCard> missions = (from m in _context.Missions
            //                              join c in _context.Cities on m.CityId equals c.CityId
            //                              join md in _context.MissionDocuments on m.MissionId equals md.MissionId
            //                              join mt in _context.MissionTypes on m.MissionTypeId equals mt.MissionTypeId
            //                              join mth in _context.MissionThemes on m.MissionThemeId equals mth.MissionThemeId
            //                              select new MissionCard()
            //                              {
            //                                  MissionId = m.MissionId,
            //                                  Title = m.Title,
            //                                  ShortDescription = m.ShortDescription,
            //                                  Description = m.Description,
            //                                  CityId = c.CityId,
            //                                  CityName = c.Name,
            //                                  DocumentName = md.DocumentName,
            //                                  DocumentPath = md.DocumentPath,
            //                                  MissionTypeId = mt.MissionTypeId,
            //                                  MissionTypeName = mt.Name,
            //                                  MissionThemeId = mth.MissionThemeId,
            //                                  MissionThemeName = mth.Title
            //                              })
            //                          .Where(m => m.Title.ToLower().Contains(filters.SearchKeyword.ToLower())).ToList();

            //if (filters.Cities != null && filters.Cities.Any())
            //{
            //    missions = (List<MissionCard>)(from mission in missions where filters.Cities.Contains(mission.CityId) select mission).ToList();
            //}

            //if (filters.Themes != null && filters.Themes.Any())
            //{
            //    missions = (List<MissionCard>)(from mission in missions where filters.Themes.Contains(mission.MissionThemeId) select mission).ToList();
            //}

            //if (filters.Skills != null && filters.Skills.Any())
            //{
            //    missions = (List<MissionCard>)(from mission in missions where filters.Skills.Contains(mission.MissionThemeId) select mission).ToList();
            //}
            #endregion
            if (missionCards.Count > 0)
            {
                ViewData["TotalPages"] = Math.Ceiling(missionCards[0].TotalRows / 6.0);
                return PartialView("_MissionCardsGrid", missionCards);
            }
            else
            {
                return PartialView("_NoMissionFound");
            }

        }


        //[HttpPost]
        public IActionResult ToggleFavouriteMission([FromBody]ToggleFavouriteFilter model)
        {
            long UserId = 8;

            try
            {
                if(model.Toggler == 1)
                {
                    FavouriteMission favouriteMission = new FavouriteMission()
                    {
                        MissionId = model.MissionId,
                        UserId = UserId,
                    };

                    _context.FavouriteMissions.Add(favouriteMission);
                    TempData["success"] = "Mission Added to favourites";
                    _context.SaveChanges();

                    ToggleFavouriteMission toggleFavouriteMission = new()
                    {
                        ButtonClassText = "Added to Favourites",
                        ButtonIconClassName = "bi-heart-fill"
                    };

                    return PartialView("Mission/_ToggleFavouriteMission", toggleFavouriteMission);

                    #region return json "parseerror"
                    //return Json(new
                    //{
                    //    status = "success",
                    //    toggleCode = 1,
                    //    message = "Mission Added to favourites"
                    //});
                    #endregion
                }
                else
                {
                    FavouriteMission? favouriteMission = _context.FavouriteMissions.FirstOrDefault(f => f.MissionId == model.MissionId && f.UserId == UserId);
                    if(favouriteMission == null)
                    {
                        TempData["error"] = "Not added to fav mission";
                        throw new Exception("Not added to fav mission");
                    }else
                    {
                       var RemovedFavMission =  _context.FavouriteMissions.Remove(favouriteMission);
                       TempData["success"] = "Removed from favourite mission";
                    }
                    ToggleFavouriteMission toggleFavouriteMission = new()
                    {
                        ButtonClassText = "Add to Favourites",
                        ButtonIconClassName = "bi-heart"
                    };
                    return PartialView("Mission/_ToggleFavouriteMission", toggleFavouriteMission);
                    #region
                    //return Json(new
                    //{
                    //    status = "success",
                    //    toggleCode = 0,
                    //    message = "Removed from favourite mission"
                    //});
                    #endregion
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "something went wrong";
                Console.WriteLine(ex.Message);
                return View();
            }
        }
    }
}