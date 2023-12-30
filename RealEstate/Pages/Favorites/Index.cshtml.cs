using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;    

namespace RealEstate.Pages.Favorites
{
    [Authorize]
    public class IndexModel : PageModel
    {
        #region Constructor
        public readonly ApplicationDbContext _Context;

        public IndexModel(ApplicationDbContext context)
        {
            _Context = context;
        }
        public List<FavoriteModel> viewmodel { get; set; }
        #endregion

        #region OnGet
        public async Task<IActionResult> OnGet(bool successfuly = false)
        {
            //if (!User.Identity.IsAuthenticated)
            //    return Redirect("/Identity/Account/Login?returnUrl=/favorites");
            var user = await _Context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            viewmodel= await _Context.Favorites.Include(e=>e.Estate)
                .Where(f=>f.UserId==user.Id).ToListAsync();
            if (viewmodel.Count == 0)
            {
                TempData["MessageType"] = "availableError";
            }
            if (successfuly == true)
            {
                TempData["MessageType"] = "success";
            }
            return Page();
        }
        #endregion
    }
}
