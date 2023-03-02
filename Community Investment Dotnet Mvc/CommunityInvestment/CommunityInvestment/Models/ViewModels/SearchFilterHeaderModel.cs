using CommunityInvestment.Entities.DataModels;

namespace CommunityInvestment.Models.ViewModels
{
    public class SearchFilterHeaderModel
    {
        public long CountryId { get; set; }
        public string CountryName { get; set; } = null!;
        public List<City> Cities { get; set; }

        public long CityId { get; set; }
        public string CityName { get; set; } = null!;

    }
}
