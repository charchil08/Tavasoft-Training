using System.ComponentModel.DataAnnotations;

namespace CommunityInvestment.Models.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="First name is required")]
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }


        [Required(ErrorMessage = "Email is Requirded")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage ="Password required"), MinLength(6, ErrorMessage ="Should be minimum of 6 characters"), MaxLength(20,ErrorMessage ="Maximum 20 characters allow")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "Phone no is required")]
        [RegularExpression("^[0-9]*$",ErrorMessage ="only numeric fields allowed")]
        public string PhoneNumber { get; set; } = null!;
    }
}
