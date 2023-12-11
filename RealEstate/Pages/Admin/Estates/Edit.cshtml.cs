using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Models.ViewModels.EstatesViewModels;
using RealEstate.Security;
using RealEstate.Services.Interface;

namespace RealEstate.Pages.Admin.Estates
{
    public class EditModel : PageModel
    {
        #region Construvtor
        private readonly ApplicationDbContext _context;
        private readonly IManagementService _managementService;

        public EditModel(ApplicationDbContext context, IManagementService managementService)
        {
            _context = context;
            _managementService = managementService;
        }
        [BindProperty]
        public EstateViewModel? ViewModel { get; set; }

        #endregion

        #region OnGet
        public async Task<IActionResult> OnGet(int Id)
        {
            if (Id <= 0)
                return NotFound();
            var estate = await _context.Estate.FindAsync(Id);
            if (estate == null)
                return NotFound();
            InitCategories();
            ViewModel.Estate = estate;
            return Page();
        }
        #endregion

        #region InitCategories
        private void InitCategories()
        {
            ViewModel = new()
            {
                CategoryOptions = new SelectList(_context.Category, nameof(CategoryModel.Id), nameof(CategoryModel.Title))
            };
        }
        #endregion

        #region OnPost
        public async Task<IActionResult> OnPost()
        {
            #region Validation
            if (!ModelState.IsValid || string.IsNullOrEmpty(ViewModel.SelectedCategory))
            {
                InitCategories();
                return Page();
            }
            bool check = int.TryParse(ViewModel.SelectedCategory, out int categoryId);
            if (check == false)
            {
                ModelState.AddModelError(string.Empty, "The selected category is invalid.");
                InitCategories();
                return Page();
            }
            var category = await _context.Category.FindAsync(categoryId);
            if (category == null)
            {
                ModelState.AddModelError(string.Empty, "The selected category is invalid.");
                InitCategories();
                return Page();
            }
            #endregion

            #region Upload
            if (ViewModel.ImgUp != null && ViewModel.ImgUp.IsImage())
            {

                if(ViewModel.Estate.Image != null)
                {
                    _managementService.deleteImg(ViewModel);
                }
                _managementService.Upload(ViewModel);
            };
            #endregion

            ViewModel.Estate.CategoryId = categoryId;
            bool editResult=_managementService.UpdateChangesAsync(ViewModel);
            var saveChangesResult = await _managementService.SaveChangesAsync();
            if (saveChangesResult == true && editResult == true)
            {
                return RedirectToPage("Index", new { successfuly = true });
            }
            else if(saveChangesResult != true || editResult != true)
            {
                return RedirectToPage("Index", new { error = true });
            }
            return Page();
        }
        #endregion

    }
}
