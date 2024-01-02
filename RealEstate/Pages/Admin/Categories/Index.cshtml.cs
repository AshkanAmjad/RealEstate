using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Services.Interface;
using RealEstate.Utilities;

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

        public PaginatedList<CategoryModel> CategoryList { get; set; }

        #endregion

        #region OnGet
        public async Task OnGetAsync(int? pageIndex ,bool successfuly = false, bool error = false)
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
                IQueryable<CategoryModel> categoryQuery= _managementService.GetCategories();
                int pageSize = 10;// Set your desired page size here
                CategoryList = await PaginatedList<CategoryModel>.CreateAsync(categoryQuery.AsNoTracking(), pageIndex ?? 1, pageSize);
            }

            if (CategoryList.Count == 0)
            {
                TempData["MessageType"] = "availableError";
            }
        }
        #endregion
    }
}
