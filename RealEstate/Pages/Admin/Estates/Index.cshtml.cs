using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;

namespace RealEstate.Pages.Admin.Estates
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<EstateModel> IndexDto { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (_context.Estate != null)
            {
                IndexDto = await _context.Estate.ToListAsync();
            }
            return Page();
        }
    }
}
