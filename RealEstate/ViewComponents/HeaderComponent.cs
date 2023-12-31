using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models.ViewModels;

namespace RealEstate.ViewComponents
{
    public class HeaderComponent:ViewComponent
    {
        public readonly ApplicationDbContext _context;
        public HeaderComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user =await _context.Users
                    .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                HeaderViewModel model = new()
                {
                    FullName = user.FullName
                };
                return View("/Pages/Shared/ViewComponents/_HeaderViewComponent.cshtml", model);
            }
            return View("/Pages/Shared/ViewComponents/_HeaderViewComponent.cshtml",new HeaderViewModel());
        }
    }
}
