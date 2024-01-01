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
using RealEstate.Utilities;

namespace RealEstate.Pages.Admin.Estates
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedList<EstateModel> EstateList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageIndex, bool successfuly = false, bool error = false, string? searchContext = null, int? selectedFilter = 0)
        {
            if (successfuly)
            {
                TempData["MessageType"] = "success";
            }
            else if (error)
            {
                TempData["MessageType"] = "error";
            }
            IQueryable<EstateModel> estateQuery = _context.Estate.OrderByDescending(e => e.DateCreated).Include(c => c.Category);

            if (searchContext != null)
            {
                if (selectedFilter == 1)
                    estateQuery = _context.Estate.OrderByDescending(e => e.DateCreated).Include(c => c.Category).Where(e => e.Category.Title == searchContext);
                else if(selectedFilter ==2)
                    estateQuery = _context.Estate.OrderByDescending(e => e.DateCreated).Include(c => c.Category).Where(e => e.Address == searchContext);
                else if (selectedFilter == 3)
                    estateQuery = _context.Estate.OrderByDescending(e => e.DateCreated).Include(c => c.Category).Where(e => e.Metrage.ToString() == searchContext);
                else if (selectedFilter == 4)
                    estateQuery = _context.Estate.OrderByDescending(e => e.DateCreated).Include(c => c.Category).Where(e => e.Price.ToString() == searchContext);

            }
            if((searchContext == null && selectedFilter != 0) || (searchContext != null && selectedFilter == 0 ))
                TempData["MessageType"] = "searchError";


            int pageSize = 10; // Set your desired page size here
            EstateList = await PaginatedList<EstateModel>.CreateAsync(estateQuery.AsNoTracking(), pageIndex ?? 1, pageSize);

            if (EstateList.Count == 0)
            {
                TempData["MessageType"] = "availableError";
            }

            return Page();
        }
    }
}