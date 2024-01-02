using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Services.Interface;

namespace RealEstate.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        private readonly IManagementService _managementService;

        public IndexModel(ApplicationDbContext context, IManagementService managementService)
        {
            _context = context;
            _managementService = managementService;
        }

        public IList<CategoryModel> CategoryModel { get;set; } = default!;
        #endregion

        #region OnGet
        public async Task OnGetAsync(bool successfuly = false, bool error = false)
        {
            if (successfuly == true)
            {
                TempData["MessageType"] = "success";
            }
            else if (error == true)
            {
                TempData["MessageType"] = "error";

            }
            if (_context.Category != null)
            {
               CategoryModel = _managementService.GetCategories();
            }
            if(CategoryModel.Count == 0)
            {
                TempData["MessageType"] = "availableError";
            }
        }
        #endregion
    }
}
