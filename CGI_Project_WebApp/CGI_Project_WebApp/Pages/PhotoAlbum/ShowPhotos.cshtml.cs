using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_DAL.repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CGI_Project_WebApp.Pages.PhotoAlbum
{
    public class ShowPhotosModel : PageModel
    {
        public PhotoAlbumService photoAlbumService = new(new PhotoAlbumRepository());

        public string CurrentLanguage { get; private set; }
        public string CountryCode { get; private set; }
        public IActionResult OnGet()
        {
            CurrentLanguage = LanguageHelper.GetCurrentLanguage(HttpContext);
            CountryCode = CurrentLanguage;
            ViewData["CurrentLanguage"] = CurrentLanguage;
            ViewData["CountryCode"] = CountryCode;

            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToPage("../Index");
            }
            return null;
        }
    }
}
