using System;
using System.Collections.Generic;

namespace CommunityInvestment.DataModels
{
    public partial class Banner
    {
        public long BannerId { get; set; }
        public string? Image { get; set; }
        public string? Text { get; set; }
        public int? SortOrder { get; set; }
        public byte[] CreatedAt { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
