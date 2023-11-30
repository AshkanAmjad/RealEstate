using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Completion of this entry is required.")]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Completion of this entry is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Completion of this entry is required.")]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Completion of this entry is required.")]
        [Display(Name = "Full name")]
        [MaxLength(200, ErrorMessage = "{0} cannot be longer than {1} characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Completion of this entry is required.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Enter your {0} correctly.")]
        public string Email { get; set; }

        public string? ReturnUrl { get; set; }

    }
}
