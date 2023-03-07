using System;
using System.Collections.Generic;

namespace CommunityInvestment.DataModels
{
    public partial class Story
    {
        public Story()
        {
            StoryInvites = new HashSet<StoryInvite>();
            StoryMedia = new HashSet<StoryMedium>();
        }

        public long StoryId { get; set; }
        public long MissionId { get; set; }
        public long UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public byte StoryStatusId { get; set; }
        public DateTime? PublishedAt { get; set; }
        public byte[] CreatedAt { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Mission Mission { get; set; } = null!;
        public virtual StoryStatus StoryStatus { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<StoryInvite> StoryInvites { get; set; }
        public virtual ICollection<StoryMedium> StoryMedia { get; set; }
    }
}
