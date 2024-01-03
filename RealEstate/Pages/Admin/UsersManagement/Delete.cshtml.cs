using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstate.Areas.Identity.Pages.Account;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Services.Interface;
using RealEstate.Utilities;

namespace RealEstate.Pages.Admin.UsersManagement
{
    public class DeleteModel : PageModel
    {
        #region Construvtor
        private readonly ApplicationDbContext _context;
        private readonly IManagementService _managementService;


        public DeleteModel(ApplicationDbContext context, IManagementService managementService)
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
            if (ViewModel.Id == null)
                return RedirectToPage("Index", new { error = true });
            var user = await _managementService.GetUserWithIdAsync(ViewModel.Id);
            if (user == null)
                return RedirectToPage("Index", new { error = true });
            var deleteResult = await _managementService.DeleteUserAsync(user);
            if (deleteResult == true)
                return RedirectToPage("Index", new { successfuly = true });
            else
                return RedirectToPage("Index", new { error = true });
        }
        #endregion
    }
}
