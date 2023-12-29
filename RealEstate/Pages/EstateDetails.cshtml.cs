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
        private readonly ApplicationDbContext _context;

        public EstateDetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public EstateDetailsViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            if(id<=0)
                return NotFound();
            var estate = await _context.Estate
                .Include(c => c.Category)
                .FirstOrDefaultAsync(e => e.Id == id);
            if(estate == null)
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
    }
}
