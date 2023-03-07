using System;
using System.Collections.Generic;

namespace CommunityInvestment.DataModels
{
    public partial class ApprovalStatus
    {
        public ApprovalStatus()
        {
            Comments = new HashSet<Comment>();
            MissionApplications = new HashSet<MissionApplication>();
            Timesheets = new HashSet<Timesheet>();
        }

        public byte ApprovalStatusId { get; set; }
        public string Name { get; set; } = null!;
        public byte[] CreatedAt { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<MissionApplication> MissionApplications { get; set; }
        public virtual ICollection<Timesheet> Timesheets { get; set; }
    }
}
