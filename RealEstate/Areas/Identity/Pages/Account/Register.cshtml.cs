#nullable disable

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Models.ViewModel;
using RealEstate.Utilities;

namespace RealEstate.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<UserModel> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<UserModel> _userManager;
        private readonly IUserStore<UserModel> _userStore;


        public RegisterModel(
            UserManager<UserModel> userManager,
            IUserStore<UserModel> userStore,
            SignInManager<UserModel> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db
            )
        {
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;
        }

        [BindProperty]
        public RegisterViewModel Input { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            Input = new()
            {
                ReturnUrl = returnUrl
            };
        }
    
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("/index");
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                await _userStore.SetUserNameAsync(user, Input.PhoneNumber, CancellationToken.None);
                user.PhoneNumber = Input.PhoneNumber;
                user.FullName = Input.FullName;
                user.Email = Input.Email;
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    #region
                    if (!await _roleManager.RoleExistsAsync(Roles.Admin))
                        await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
                    if (!await _roleManager.RoleExistsAsync(Roles.User))
                        await _roleManager.CreateAsync(new IdentityRole(Roles.User));
                    if (_db.Users.ToList().Count == 1)
                        await _userManager.AddToRoleAsync(user, Roles.Admin);
                    else
                        await _userManager.AddToRoleAsync(user, Roles.User);
                    #endregion

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToPage(returnUrl, new { successfuly = true });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }

        private UserModel CreateUser()
        {
            try
            {
                return Activator.CreateInstance<UserModel>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }


    }
}
