using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Services.Implementation;
using RealEstate.Services.Interface;

namespace RealEstate.Pages.Admin.Categories
{
    public class DeleteModel : PageModel
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        private readonly IManagementService _managementService;

        public DeleteModel(ApplicationDbContext context, IManagementService managementService)
        {
            _context = context;
            _managementService = managementService;
        }

        [BindProperty]
        public CategoryModel CategoryModel { get; set; } = default!;
        #endregion

        #region OnGet
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorymodel = _managementService.GetCategoryWithId(id);

            if (categorymodel == null)
            {
                return NotFound();
            }
            CategoryModel = categorymodel;
            return Page();
        }
        #endregion

        #region OnPost
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorymodel = _managementService.GetCategoryWithId(id);

            if (categorymodel != null)
            {
                CategoryModel = categorymodel;
                var deleteResult = _managementService.DeleteCategory(CategoryModel);
                var saveChangesResult = await _managementService.SaveChangesAsync();
                if (deleteResult == true && saveChangesResult == true)
                {
                    return RedirectToPage("Index", new { successfuly = true });
                }
                else if (deleteResult != true || saveChangesResult != true)
                {
                    return RedirectToPage("Index", new { error = true });
                }
            }
            return Page();
        }
        #endregion
    }
}
