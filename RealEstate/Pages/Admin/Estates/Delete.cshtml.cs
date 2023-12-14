using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Models.ViewModels.EstatesViewModels;
using RealEstate.Services.Interface;

namespace RealEstate.Pages.Admin.Estates
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
        public EstateModel? ViewModel { get; set; }
        #endregion

        #region OnGet
        public async Task<IActionResult> OnGet(int Id)
        {
            if (Id <= 0)
                return NotFound();
            ViewModel = await _context.Estate.Include(c=>c.Category).FirstOrDefaultAsync(e=>e.Id==Id);
            if (ViewModel == null)
                return NotFound();
            return Page();
        }
        #endregion

        #region OnPost
        public async Task<IActionResult> OnPost()
        {
            if(ViewModel.Id <= 0)
                return NotFound();
            if (ViewModel.Image != "default-image.png")
            {
                _managementService.deleteImg(ViewModel);
            };
            bool deleteResult= _managementService.DeleteEstate(ViewModel);
            var saveChangesResult = await _managementService.SaveChangesAsync();
            if (deleteResult == true && saveChangesResult == true )
            {
                return RedirectToPage("Index", new { successfuly = true });
            }
            else if (deleteResult != true || saveChangesResult != true )
            {
                return RedirectToPage("Index", new { error = true });
            }
            return Page();
        }


        #endregion
    }
}

