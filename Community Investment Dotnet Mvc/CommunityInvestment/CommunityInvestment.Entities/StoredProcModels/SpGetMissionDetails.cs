using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityInvestment.Entities.StoredProcModels
{
    public class SpGetMissionDetails
    {
		public long MissionId { get; set; } 
		public string Title { get; set; } = String.Empty;
		public string ShortDesc { get; set; } =	String.Empty;
		public string Description { get; set; } = String.Empty;

		//--Time_mission

		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public int? SeatsLeft { get; set; }
		public DateTime? Deadline { get; set; }

		//--Goal mission
		public string? GoalObjectiveText { get; set; }
		public int? GoalValue { get; set; }
		public int? AchievedGoalValue { get; set; }

		public long CityId { get; set; }
		public string CityName { get; set; } = String.Empty;
		public long ThemeId { get; set; }

		public string ThemeName { get; set; } = String.Empty;
		public string OrganizationName { get; set; } = String.Empty;

		//--Skill
		public List<SpSkill>? SpSkills { get; set; }
		
		//--availability

		public Byte AvailabilityId { get; set; }
		public string AvailibilityDays { get; set; } = String.Empty;

		//--rating & (user ramianing)
		public decimal Rating { get; set; } = 0; 
    }
}
