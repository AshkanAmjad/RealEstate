using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Models.ViewModels.EstatesViewModels;
using RealEstate.Security;
using RealEstate.Services.Implementation;
using RealEstate.Services.Interface;

namespace RealEstate.Pages.Admin.Estates
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
        public EstateViewModel? ViewModel { get; set; }
        #endregion

        #region OnGet
        public void OnGet()
        {
            InitCategories();
            
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
                InitCategories() ;
                return Page();
            }
            bool check=int.TryParse(ViewModel.SelectedCategory, out int categoryId);
            if (check == false) 
            {
                ModelState.AddModelError(string.Empty, "The selected category is invalid.");
                InitCategories();
                return Page();
            }
            var category=await _context.Category.FindAsync(categoryId);
            if (category == null)
            {
                ModelState.AddModelError(string.Empty, "The selected category is invalid.");
                InitCategories();
                return Page();
            }
            #endregion

            #region Upload
            if(ViewModel.ImgUp != null && ViewModel.ImgUp.IsImage())
            {
               _managementService.Upload(ViewModel);
            }
            #endregion

            ViewModel.Estate.CategoryId = categoryId;
            var addResult = await _managementService.CreateEstatesAsync(ViewModel);
            var saveChangesResult = await _managementService.SaveChangesAsync();

            if (addResult ==true && saveChangesResult == true)
            {
                return RedirectToPage("Index", new { successCreated = true });
            }
            else if (addResult != true || saveChangesResult != true)
            {
                return RedirectToPage("Index", new { errorCreated = true });
            }
            return Page();
        }

        #endregion
    }
}
