// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealEstate.Models;
using RealEstate.Models.ViewModel;

namespace RealEstate.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<UserModel> _signInManager;

        public LoginModel(SignInManager<UserModel> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            Input = new();
            if (!string.IsNullOrEmpty(Input.ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, Input.ErrorMessage);
            }

            Input = new()
            {
                ReturnUrl = returnUrl
            };

            returnUrl ??= Url.Content("~/");

            if(User.Identity.IsAuthenticated)
            {
                LocalRedirect(returnUrl);
            }

            Input.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                returnUrl ??= Url.Content("/index");
                var result = await _signInManager.PasswordSignInAsync(Input.phoneNumber, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                return RedirectToPage(returnUrl, new { successfuly = true });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }
            return Page();
        }
    }
}
