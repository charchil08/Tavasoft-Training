using CommunityInvestment.Entities.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommunityInvestment.Models.ViewModels
{
    public class SearchFilterHeaderModel
    {
        public IEnumerable<SelectListItem> CountryList { get; set; }

        public IEnumerable<City>? CityList { get; set; }

    }
}
