using CommunityInvestment.Entities.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommunityInvestment.Models.ViewModels
{
    public class SearchFilterHeaderModel
    {
        public IEnumerable<SelectListItem> CountryList { get; set; } = null!;

        public long? CountryId { get; set; }

        public IEnumerable<City>? CityList { get; set; }

        public IEnumerable<MissionTheme> MissionThemesList { get; set; } = null!;

        public IEnumerable<Skill> SkillsList { get; set; } = null!;

    }
}
