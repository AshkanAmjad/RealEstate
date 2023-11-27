using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class UserModel:IdentityUser
    {
        [Required(ErrorMessage ="تکمیل این ورودی اجباری است.")]
        [Display(Name ="نام و نام خانوادگی")]
        [MaxLength(200,ErrorMessage ="ورودی {0} نمی تواند بیشتر از {1} حرف باشد.")]
        public string FullName { get; set; }
    }
}
