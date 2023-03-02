using System.ComponentModel.DataAnnotations;

namespace CommunityInvestment.Models.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is Requirded")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                           ErrorMessage = "Email is not valid")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; } = null!;
    }
}
