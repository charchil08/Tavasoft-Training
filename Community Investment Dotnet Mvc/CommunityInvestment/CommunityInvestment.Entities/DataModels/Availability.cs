using System;
using System.Collections.Generic;

namespace CommunityInvestment.Entities.DataModels
{
    public partial class Availability
    {
        public Availability()
        {
            Missions = new HashSet<Mission>();
        }

        public byte AvailabilityId { get; set; }
        public string? Name { get; set; }
        public byte[] CreatedAt { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Mission> Missions { get; set; }
    }
}
