using System;
using System.Collections.Generic;

namespace CommunityInvestment.Model.Models
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
            Missions = new HashSet<Mission>();
            Users = new HashSet<User>();
        }

        public long CountryId { get; set; }
        public string Name { get; set; } = null!;
        public string Iso { get; set; } = null!;
        public byte[] CreatedAt { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Mission> Missions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
