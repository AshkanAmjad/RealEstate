using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Services.Implementation;
using RealEstate.Services.Interface;

namespace RealEstate.Pages.Admin.Categories
{
    public class DetailsModel : PageModel
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        private readonly IManagementService _managementService;

        public DetailsModel(ApplicationDbContext context, IManagementService managementService)
        {
            _context = context;
            _managementService = managementService;
        }

        public CategoryModel CategoryModel { get; set; } = default!;
        #endregion

        #region OnGet
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorymodel = await _context.Category.FirstOrDefaultAsync(m => m.Id == id);
            if (categorymodel == null)
            {
                return NotFound();
            }
            CategoryModel = categorymodel;
            return Page();
        }
        #endregion
    }
}
