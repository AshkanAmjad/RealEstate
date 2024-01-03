using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Services.Interface;
using RealEstate.Utilities;

namespace RealEstate.Pages.Admin.UsersManagement
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
        public PaginatedList<UserModel> UsersList { get; set; }


        #endregion

        #region OnGet
        public async Task<IActionResult> OnGetAsync(int? pageIndex, bool successfuly = false, bool error = false, string? searchContext = null,
            bool similarityError = false,int? selectedFilter = 0)
        {
            if (successfuly)
                TempData["MessageType"] = "success";
            else if (error)
                TempData["MessageType"] = "error";
            else if(similarityError)
                TempData["MessageType"] = "similarityError";


            IQueryable<UserModel> usersQuery = _managementService.GetUsers();
            if (searchContext != null && selectedFilter != 0)
                usersQuery = _managementService.FilterUsers(searchContext, selectedFilter);
            if ((searchContext == null && selectedFilter != 0) || (searchContext != null && selectedFilter == 0))
                TempData["MessageType"] = "searchError";

            int pageSize = 10; // Set your desired page size here
            UsersList = await PaginatedList<UserModel>.CreateAsync(usersQuery.AsNoTracking(), pageIndex ?? 1, pageSize);
            if (UsersList.Count == 0)
            {
                TempData["MessageType"] = "availableError";
            }
            return Page();
        }
        #endregion
    }
}
