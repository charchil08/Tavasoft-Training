using System;
using System.Collections.Generic;

namespace CommunityInvestment.DataModels
{
    public partial class MissionType
    {
        public MissionType()
        {
            Missions = new HashSet<Mission>();
        }

        public byte MissionTypeId { get; set; }
        public string? Name { get; set; }
        public byte[] CreatedAt { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Mission> Missions { get; set; }
    }
}
