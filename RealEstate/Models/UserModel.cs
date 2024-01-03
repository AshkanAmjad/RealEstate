using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class UserModel:IdentityUser
    {
        [Required(ErrorMessage = "Completion of this entry is required.")]
        [Display(Name ="Full name ")]
        [MaxLength(200, ErrorMessage = "{0} cannot be longer than {1} characters.")]
        public string FullName { get; set; }
        public DateTime SignUpDate { get; set; }=DateTime.Now;
    }
}
