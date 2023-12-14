using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Services.Implementation;
using RealEstate.Services.Interface;

namespace RealEstate.Pages.Admin.Categories
{
    public class CreateModel : PageModel
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        private readonly IManagementService _managementService;

        public CreateModel(ApplicationDbContext context, IManagementService managementService)
        {
            _context = context;
            _managementService = managementService;
        }
        [BindProperty]
        public CategoryModel CategoryModel { get; set; } = default!;
        #endregion

        #region OnGet
        public IActionResult OnGet()
        {
            return Page();
        }
        #endregion


        #region OnPost
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool addResult=await _managementService.AddCategoriesToDBAsync(CategoryModel);
            bool saveChangesResult=await _managementService.SaveChangesAsync();
            if (addResult == true && saveChangesResult == true)
            {
                return RedirectToPage("Index", new { successfuly = true });
            }
            else if (addResult != true || saveChangesResult != true)
            {
                return RedirectToPage("Index", new { error = true });
            }
            return Page();
        }
        #endregion
    }
}
