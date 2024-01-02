using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Services.Interface;
using RealEstate.Utilities;

namespace RealEstate.Pages.Favorites
{
    [Authorize]
    public class IndexModel : PageModel
    {
        #region Constructor
        public readonly ApplicationDbContext _context;
        private readonly IManagementService _managementService;

        public IndexModel(ApplicationDbContext context,IManagementService managementService)
        {
            _context = context;
            _managementService = managementService;
        }
        public PaginatedList<FavoriteModel> FavoriteList { get; set; }

        #endregion

        #region OnGet
        public async Task<IActionResult> OnGet(int? pageIndex,bool successfuly = false)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            IQueryable<FavoriteModel> favoritesQuery = _managementService.GetFavoritesByUserId(user);

            if (successfuly == true)
                TempData["MessageType"] = "success";
            
            int pageSize = 12;// Set your desired page size here
            FavoriteList = await PaginatedList<FavoriteModel>.CreateAsync(favoritesQuery.AsNoTracking(), pageIndex ?? 1, pageSize);

            if (FavoriteList.Count == 0)
                TempData["MessageType"] = "availableError";
            return Page();
        }
        #endregion
    }
}
