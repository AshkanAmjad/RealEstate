using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Services.Interface;
using RealEstate.Utilities;

namespace RealEstate.Pages
{
    public class AllEstatesModel : PageModel
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        private readonly IManagementService _managementService;


        public AllEstatesModel(ApplicationDbContext context, IManagementService managementService)
        {
            _context = context;
            _managementService = managementService;
        }

        public PaginatedList<EstateModel> EstateList { get; set; }

        #endregion

        #region OnGet
        public async Task<IActionResult> OnGetAsync(int? pageIndex, bool successfuly = false, bool error = false, string? searchContext = null, int? selectedFilter = 0)
        {
            IQueryable<EstateModel> estateQuery = _managementService.GetEstates();
            if (searchContext != null && selectedFilter != 0)
                estateQuery = _managementService.FilterEstates(searchContext, selectedFilter);

            if ((searchContext == null && selectedFilter != 0) || (searchContext != null && selectedFilter == 0))
                TempData["MessageType"] = "searchError";

            int pageSize = 12; // Set your desired page size here
            EstateList = await PaginatedList<EstateModel>.CreateAsync(estateQuery.AsNoTracking(), pageIndex ?? 1, pageSize);

            if (EstateList.Count == 0)
                TempData["MessageType"] = "availableError";
            if (successfuly)
                TempData["MessageType"] = "success";
            else if (error)
                TempData["MessageType"] = "error";

            return Page();
        }
        #endregion
    }
}