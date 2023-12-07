using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_DAL.repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CGI_Project_WebApp.Pages.PhotoAlbum
{
    public class ShowPhotosModel : PageModel
    {
        public PhotoAlbumService photoAlbumService = new(new PhotoAlbumRepository());

        public void OnGet()
        {
        }
    }
}
