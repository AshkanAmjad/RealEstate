using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;

namespace RealEstate.Pages
{
    public class AllEstatesModel : PageModel
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        public AllEstatesModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<EstateModel>? viewmodel { get; set; }
        #endregion

        #region OnGet
        public async Task<IActionResult> OnGet(bool successfuly = false, bool error = false)
        {
            if (successfuly == true)
            {
                TempData["MessageType"] = "success";
            }
            else if (error == true)
            {
                TempData["MessageType"] = "error";

            }
            viewmodel = await _context.Estate.OrderByDescending(e => e.DateCreated).Include(c => c.Category).ToListAsync();
            if (viewmodel.Count == 0)
            {
                TempData["MessageType"] = "availableError";
            }
            return Page();
        }
        #endregion
    }
}

