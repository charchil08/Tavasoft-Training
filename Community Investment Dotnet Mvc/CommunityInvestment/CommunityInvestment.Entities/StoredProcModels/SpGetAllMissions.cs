using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityInvestment.Entities.StoredProcModels
{
	//SpGetAllMissions
    public partial class SpGetAllMissions
    {
		public long MissionId { get; set; }
		public string Title { get; set; } = null!;
		public string ShortDesc { get; set; } = null!;
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string OrganizationName { get; set; } = null!;

		public long CityId { get; set; }
		public string CityName { get; set; } = null!;

		public string DocumentName { get; set; } = null!;
		public string DocumentPath { get; set; } = null!;

		public byte TypeId { get; set; }
		public string TypeName	{ get; set; } = null!;

		public long ThemeId { get; set; }
		public string ThemeName { get; set; } = null!;

		public int TotalSeat { get; set; }
		public int EnrolledUser { get; set; }
		public DateTime Deadline { get; set; }

		public decimal MissionRating { get; set; } = 0;

		public long RowNo { get; set; }
		public long TotalRows { get; set; }

    }
}

//mission_id bigint,
//		title varchar(128),
//		short_desc varchar(128),
//		start_date datetime,
//		end_date datetime,
//		city_id bigint,
//		city_name varchar(255),
//		document_name varchar(255),
//		document_path varchar(255),
//		[type_id] tinyint,
//		[type_name] varchar(16),
//		[theme_id] bigint,
//		[theme_name] varchar(128),

//		--for time mission
//		total_seat int,
//		enrolled_user int,
//		deadline datetime