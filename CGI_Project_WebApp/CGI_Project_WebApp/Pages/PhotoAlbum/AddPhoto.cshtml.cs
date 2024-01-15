using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_DAL.repositories;
using CGI_Project_WebApp_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

namespace CGI_Project_WebApp.Pages.PhotoAlbum
{
    public class AddPhotoModel : PageModel
    {
        IWebHostEnvironment env;

        [BindProperty]
        public Photos photo {get; set;}
        public IFormFile Upload { get; set; }

        public FileChecker checker { get; set; } = new FileChecker();

        public PhotoAlbumService photoAlbumService = new(new PhotoAlbumRepository());

        public Error Error = new();
        public string CurrentLanguage { get; private set; }
        public string CountryCode { get; private set; }
        private readonly IStringLocalizer<AddPhotoModel> _stringLocalizer;

        public AddPhotoModel(IWebHostEnvironment web, IStringLocalizer<AddPhotoModel> stringLocalizer)
        {
            env = web;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<IActionResult> OnGet()
        {
            CurrentLanguage = LanguageHelper.GetCurrentLanguage(HttpContext);
            CountryCode = CurrentLanguage;
            ViewData["CurrentLanguage"] = CurrentLanguage;
            ViewData["CountryCode"] = CountryCode;

            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToPage("../Index");
            }
            //Wait for half a second before resetting properties
            await Task.Delay(500);

            //Reset all properties
            Error.ResetErrorHandling();
            ResetButton();
            return null;
        }

        public async Task<IActionResult> OnPostTryAddPhoto()
        {
            if (HttpContext.Session.GetInt32("ActivityButton") == 0)
            {
                return RedirectToPage("AddPhoto"); // Redirect if the button is already clicked
            }

            HttpContext.Session.SetInt32("ActivityButton", 0);

            var path = Path.Combine(env.WebRootPath, "PhotoAlbum");

            if (Upload == null)
            {
                Error.HandleError("PhotoInvalid");
                return;
            }

            FileUploadPreCheckValue preCheckResult = checker.TestFile(Upload);

            if (preCheckResult == FileUploadPreCheckValue.TooLarge)
            {
                TempData["Error"] = "Photo is too large";
                ResetButton();
                return RedirectToPage();
            }

            if (preCheckResult == FileUploadPreCheckValue.NoValidFIleType)
            {
                TempData["Error"] = "Photo-type is not valid";
                ResetButton();
                return RedirectToPage();
            }

            await photoAlbumService.TryAddPhoto(photo, path, Upload);

            TempData["Success"] = "Successfully added the photo";

            return RedirectToPage("AddPhoto");
        }

        private void ResetButton()
        {
            HttpContext.Session.SetInt32("ActivityButton", 1);
        }
    }
}
