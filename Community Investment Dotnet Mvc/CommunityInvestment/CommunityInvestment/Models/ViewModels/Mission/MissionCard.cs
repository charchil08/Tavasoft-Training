namespace CommunityInvestment.Models.ViewModels.Mission
{
    public class MissionCard
    {
        public long MissionId { get; set; } = 0!;
        public string Title { get; set; } = null!;
        public string? ShortDescription { get; set; } 
        
        public long CityId { get; set; } = 0!;
        public string CityName { get; set; } = null!;
        
        public string DocumentName { get; set; } = null!;
        public string? DocumentPath { get; set; } 

        public byte TypeId { get; set; } = 0!;
        public string? TypeName { get; set; } 

        public long ThemeId { get; set; } = 0!;
        public string ThemeName { get; set; } = null!;
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