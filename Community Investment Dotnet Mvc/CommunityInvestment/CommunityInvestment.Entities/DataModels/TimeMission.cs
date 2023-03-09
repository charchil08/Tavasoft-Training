using System;
using System.Collections.Generic;

namespace CommunityInvestment.Entities.DataModels
{
    public partial class TimeMission
    {
        public long TimeMissionId { get; set; }
        public long MissionId { get; set; }
        public int? TotalSeat { get; set; }
        public int? EnrolledUser { get; set; }
        public DateTime? Deadline { get; set; }
        public byte[] CreatedAt { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Mission Mission { get; set; } = null!;
    }
}
