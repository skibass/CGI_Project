using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_DAL.repositories;
using CGI_Project_WebApp_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CGI_Project_WebApp.Pages.PhotoAlbum
{
    public class AddPhotoModel : PageModel
    {
        IWebHostEnvironment env;

        [BindProperty]
        public Photos photo {get; set;}
        public IFormFile Upload { get; set; }

        public PhotoAlbumService photoAlbumService = new(new PhotoAlbumRepository());

        public AddPhotoModel(IWebHostEnvironment web)
        {
            env = web;
        }
        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToPage("../Index");
            }
            return null;
        }

        public async Task OnPostTryAddPhoto()
        {
            var path = Path.Combine(env.WebRootPath, "PhotoAlbum");

            await photoAlbumService.TryAddPhoto(photo, path, Upload);

            RedirectToPage();
        }
    }
}
