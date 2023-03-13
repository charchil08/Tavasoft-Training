namespace CommunityInvestment.Models.ViewModels.Mission
{
    public class Filters
    {
        public string SearchKeyword { get; set; } = string.Empty;
        public List<long>? Themes { get; set; }
        public List<long>? Skills { get; set; }
        public List<long>? Cities { get; set; }
        public List<long>? Countries { get; set; }
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set;}
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 6;
    }
}
