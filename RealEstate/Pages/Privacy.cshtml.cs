using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealEstate.Utilities;

namespace RealEstate.Pages
{
    [Authorize(Roles = Roles.Admin)]
    public class PrivacyModel : PageModel
    {

        public PrivacyModel()
        {
        }

        public void OnGet()
        {
        }
    }
}