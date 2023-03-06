namespace CommunityInvestment.Models.ViewModels.Mission
{
    public class MissionCard
    {
        public long MissionId { get; set; } = 0!;
        public string Title { get; set; } = null!;
        public string? ShortDescription { get; set; } 
        public string? Description { get; set; }
        
        public long CityId { get; set; } = 0!;
        public string CityName { get; set; } = null!;
        
        public string DocumentName { get; set; } = null!;
        public string? DocumentPath { get; set; } 

        public byte MissionTypeId { get; set; } = 0!;
        public string? MissionTypeName { get; set; } 

        public long MissionThemeId { get; set; } = 0!;
        public string MissionThemeName { get; set; } = null!;
    }
}

//c.CityId,
//                               CityName = c.Name,
//                               md.DocumentName,
//                               md.DocumentPath,
//                               mt.MissionTypeId,
//                               MissionTypeName = mt.Name,
//                               mth.MissionThemeId,
//                               MissionThemeName = mth.Title