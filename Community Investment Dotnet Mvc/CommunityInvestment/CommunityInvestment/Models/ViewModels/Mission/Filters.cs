namespace CommunityInvestment.Models.ViewModels.Mission
{
    public class Filters
    {
        public string SearchKeyword { get; set; } = string.Empty;
        public List<long>? Themes { get; set; }
        public List<long>? Skills { get; set; }
        public List<long>? Cities { get; set; }
    }
}
