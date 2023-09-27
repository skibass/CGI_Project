using CGI_Models;
using CGI_DAL;
using CGI_DAL.Database_Models;

namespace CGI_BLL
{
    public class Class1
    {
        Dbi511119Context context = new Dbi511119Context();
        public string Test()
        {
            string companyTest = "";

            foreach (var item in context.Companies)
            {
                companyTest += item.Name + "";
            }
            return companyTest;
        }
    }
}