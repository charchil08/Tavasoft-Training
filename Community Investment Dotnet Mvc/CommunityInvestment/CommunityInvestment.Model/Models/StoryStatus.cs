using System;
using System.Collections.Generic;

namespace CommunityInvestment.Model.Models
{
    public partial class StoryStatus
    {
        public StoryStatus()
        {
            Stories = new HashSet<Story>();
        }

        public byte StoryStatusId { get; set; }
        public string Name { get; set; } = null!;
        public byte[] CreatedAt { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Story> Stories { get; set; }
    }
}
