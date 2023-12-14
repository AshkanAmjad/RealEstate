using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RealEstate.Pages
{
    public class IndexModel : PageModel
    {
        #region Constructor
        public IndexModel()
        {
        }
        #endregion

        #region OnGet
        public void OnGet(bool successfuly = false, bool error = false)
        {
            if (successfuly == true)
            {
                TempData["MessageType"] = "success";
            }
            else if (error == true)
            {
                TempData["MessageType"] = "error";

            }
        }
        #endregion
    }
}