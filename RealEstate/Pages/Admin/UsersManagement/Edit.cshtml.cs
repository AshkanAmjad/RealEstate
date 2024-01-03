using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Services.Interface;

namespace RealEstate.Pages.Admin.UsersManagement
{
    public class CreateModel : PageModel
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        private readonly IManagementService _managementService;

        public CreateModel(IManagementService managementService, ApplicationDbContext context)
        {
            _context = context;
            _managementService = managementService;
        }

        [BindProperty]
        public UserModel ViewModel { get; set; }
        #endregion

        #region OnGet
        public async Task<IActionResult> OnGet(string Id)
        {
            if (Id == "")
                return RedirectToPage("Index", new { error = true });
            ViewModel = await _managementService.GetUserWithIdAsync(Id);
            if (ViewModel == null)
                return RedirectToPage("Index", new { error = true });
            return Page();
        }
        #endregion

        #region OnPost
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            var user = await _managementService.GetUserWithIdAsync(ViewModel.Id);
            if (user == null)
                return RedirectToPage("Index", new { error = true });

            if((user.FullName != ViewModel.FullName) && (_context.Users.Any(u=>u.FullName == ViewModel.FullName)))
                return RedirectToPage("Index", new { similarityError = true });
            else if ((user.Email != ViewModel.Email) && (_context.Users.Any(u => u.Email == ViewModel.Email)))
                return RedirectToPage("Index", new { similarityError = true });
            else if ((user.PhoneNumber != ViewModel.PhoneNumber) && (_context.Users.Any(u => u.PhoneNumber == ViewModel.PhoneNumber)))
                return RedirectToPage("Index", new { similarityError = true });

            user.Email = ViewModel.Email;
            user.PhoneNumber = ViewModel.PhoneNumber;
            user.FullName = ViewModel.FullName;
            var updateResult = await _managementService.UpdateUserAsync(user);
            if (updateResult == true)
                return RedirectToPage("Index", new { successfuly = true });
            else
                return RedirectToPage("Index", new { error = true });
        }
        #endregion
    }
}
