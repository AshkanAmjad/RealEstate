using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using RealEstate.Data;
using RealEstate.Models;

namespace RealEstate.Pages.Admin.Estates
{
    public class IndexModel : PageModel
    {
        #region Constructor
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<EstateModel> IndexDto { get;set; } = default!;
        #endregion

        #region OnGet
        public async Task<IActionResult> OnGetAsync(bool successCreated = false,bool errorCreated=false
            ,bool successEdited=false,bool errorEdited=false)
        {
            ViewData["successCreated"] = successCreated;
            ViewData["errorCreated"] = errorCreated;
            ViewData["successEdited"] = successEdited;
            ViewData["errorEdited"] = errorEdited;

            if (_context.Estate != null)
            {
                IndexDto = await _context.Estate.Include(c=>c.Category).ToListAsync();
            }

            return Page();
        }
        #endregion
    }
}
