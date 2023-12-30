using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Models.ViewModels.EstatesViewModels;

namespace RealEstate.Pages
{
    public class EstateDetailsModel : PageModel
    {
        #region Constructor
        private readonly ApplicationDbContext _context;

        public EstateDetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public EstateDetailsViewModel ViewModel { get; set; }
        #endregion

        #region OnGet
        public async Task<IActionResult> OnGet(int id)
        {
            if (id <= 0)
                return NotFound();
            var estate = await _context.Estate
                .Include(c => c.Category)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (estate == null)
                return NotFound();
            ViewModel = new()
            {
                Estate = estate,
                SuggestedEstates = _context.Estate
                .Include(c => c.Category)
                .Where(e => e.Id != estate.Id && e.Category.Title == estate.Category.Title)
                .Take(3)
                .ToList()
            };
            return Page();
        }
        #endregion

        #region OnPost
        public async Task<IActionResult> OnPostAddToFavorites(int id)
        {
            if (User is null || !User.Identity.IsAuthenticated)
                return RedirectToPage("Identity/Account/Login?returnUrl=/EstateDetails?id=" + id);
            if (id <= 0)
                return NotFound();
            var estate = await _context.Estate.FirstOrDefaultAsync(e => e.Id == id);
            if (estate == null)
                return NotFound();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var check = await _context.Favorites.FirstOrDefaultAsync(f => f.UserId == user.Id && f.EstateId == id);
            if (check == null)
            {
                await _context.AddAsync(new FavoriteModel()
                {
                    EstateId= id,
                    UserId = user.Id
                });
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("EstateDetails", new { id });
        }
        #endregion
    }
}
