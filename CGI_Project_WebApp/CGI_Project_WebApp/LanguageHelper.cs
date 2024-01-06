namespace CGI_Project_WebApp
{
    public class LanguageHelper
    {
        public static string GetDefaultLanguage() => "EN";

        public static string GetCurrentLanguage(HttpContext httpContext)
        {
            var currentLanguage = httpContext.Request.Cookies[".AspNetCore.Culture"] ?? GetDefaultLanguage();
            var parts = currentLanguage.Split('|');
            return parts.Length >= 2 ? parts[0].Substring(2) : GetDefaultLanguage();
        }
    }
}
