using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Services.Implementation;
using RealEstate.Services.Interface;

namespace RealEstate.Pages.Admin.Categories
{
    public class EditModel : PageModel
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        private readonly IManagementService _managementService;

        public EditModel(ApplicationDbContext context,IManagementService managementService)
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
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }
            var categorymodel =  await _context.Category.FirstOrDefaultAsync(m => m.Id == id);
            if (categorymodel == null)
            {
                return NotFound();
            }
            CategoryModel = categorymodel;
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

            bool editResult = _managementService.UpdateChanges(CategoryModel);
            var saveChangesResult = await _managementService.SaveChangesAsync();
            if (saveChangesResult == true && editResult == true)
            {
                return RedirectToPage("Index", new { successfuly = true });
            }
            else if (saveChangesResult != true || editResult != true)
            {
                return RedirectToPage("Index", new { error = true });
            }
            return Page();
        }
        #endregion

    }
}
