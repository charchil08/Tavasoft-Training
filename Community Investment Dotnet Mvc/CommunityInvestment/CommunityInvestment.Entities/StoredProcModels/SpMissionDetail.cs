using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityInvestment.Entities.StoredProcModels
{
    public class SpMissionDetail
    {
		public long MissionId { get; set; } 
		public string Title { get; set; } = null!;
		public string ShortDesc { get; set; } =	null!;
		public string Description { get; set; } = null!;

		//--Time_mission

		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public int? EnrolledUser { get; set; }
		public DateTime? Deadline { get; set; }

		//--Goal mission
		public string? GoalObjectiveText { get; set; }
		public int? SeatsLeft { get; set; }
		public int? AchievedGoalValue { get; set; }

		public long CityId { get; set; }
		public string CityName { get; set; } = null!;
		
		public long ThemeId { get; set; }
        public string ThemeName { get; set; } = null!;
		public string OrganizationName { get; set; } = null!;

		//--Skill
		public string? SkillName { get; set; }

		//--availability
		public byte AvailabilityId { get; set; }
		public string AvailibilityDays { get; set; } = null!;

		//--rating & (user ramianing)
		public decimal Rating { get; set; } = 0; 

		//Fav mission or not
		public bool IsFavouriteMission { get; set; }
    }
}
