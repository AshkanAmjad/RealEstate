using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Models.ViewModels.EstatesViewModels;
using RealEstate.Services.Interface;

namespace RealEstate.Pages.Admin.Estates
{
    public class DetailsModel : PageModel
    {
        #region Construvtor
        private readonly ApplicationDbContext _context;
        private readonly IManagementService _managementService;

        public DetailsModel(ApplicationDbContext context, IManagementService managementService)
        {
            _context = context;
            _managementService = managementService;
        }
        [BindProperty]
        public EstateModel? ViewModel { get; set; }
        #endregion

        #region OnGet
        public async Task<IActionResult> OnGet(int Id)
        {
            if (Id <= 0)
                return NotFound();
            ViewModel = _managementService.GetEstateAndCategoryWithId(Id);
            if (ViewModel == null)
                return NotFound();
            return Page();
        }
        #endregion
    }
}
