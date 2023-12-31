using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Utilities;

namespace RealEstate.Pages
{
    public class AllEstatesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AllEstatesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedList<EstateModel> EstateList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageIndex, bool successfuly = false, bool error = false)
        {
            IQueryable<EstateModel> estateQuery = _context.Estate
                .OrderByDescending(e => e.DateCreated)
                .Include(c => c.Category);

            int pageSize = 12; // Set your desired page size here
            EstateList = await PaginatedList<EstateModel>.CreateAsync(estateQuery.AsNoTracking(), pageIndex ?? 1, pageSize);

            if (EstateList.Count == 0)
            {
                TempData["MessageType"] = "availableError";
            }
            if (successfuly)
            {
                TempData["MessageType"] = "success";
            }
            else if (error)
            {
                TempData["MessageType"] = "error";
            }

            return Page();
        }
    }
}