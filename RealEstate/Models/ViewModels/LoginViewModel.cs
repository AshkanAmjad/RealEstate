using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Phone number")]
        public string phoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }

        [TempData]
        public string? ErrorMessage { get; set; }
    }
}
