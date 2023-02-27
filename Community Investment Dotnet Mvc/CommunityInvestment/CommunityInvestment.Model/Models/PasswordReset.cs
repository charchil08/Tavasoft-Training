using System;
using System.Collections.Generic;

namespace CommunityInvestment.Model.Models
{
    public partial class PasswordReset
    {
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        public byte[] CreatedAt { get; set; } = null!;
    }
}
