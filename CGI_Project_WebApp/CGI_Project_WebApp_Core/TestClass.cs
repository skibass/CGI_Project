using CGI_Project_WebApp_DAL.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Core
{
    public class TestClass
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
