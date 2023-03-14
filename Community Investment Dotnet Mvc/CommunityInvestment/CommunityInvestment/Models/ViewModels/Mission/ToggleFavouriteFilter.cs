namespace CommunityInvestment.Models.ViewModels.Mission
{
    public class ToggleFavouriteFilter
    {
        public long MissionId { get; set; }
        public byte Toggler { get; set; } //1 for insert - 0 for delete
    }
}
