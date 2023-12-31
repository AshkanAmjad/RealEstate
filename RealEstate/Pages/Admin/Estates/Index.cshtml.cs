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
        public async Task<IActionResult> OnGetAsync(bool successfuly =false,bool error = false)
        {
            
            if(successfuly == true)
            {
                TempData["MessageType"] = "success";
            }
            else if(error == true) 
            {
                TempData["MessageType"] = "error";

            }

            if (_context.Estate != null)
            {
                IndexDto = await _context.Estate.OrderByDescending(e=>e.DateCreated).Include(c => c.Category).ToListAsync();
            }
            if(IndexDto.Count == 0)
            {
                TempData["MessageType"] = "availableError";
            }

            return Page();
        }
        #endregion
    }
}
