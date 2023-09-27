using CGI_DAL.Database_Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using CGI_Models;

namespace CGI_DAL.repositories
{
    internal class EmployeeRepository
    {
        public List<Employee> GetEmployees(int companyId)
        {
            Dbi511119Context context= new Dbi511119Context();

            List<Employee> employees= context.Companies.Where(c => c.Id == companyId).FirstOrDefault().Employees.ToList();
            if(employees == null)
            {
                employees= new List<Employee>();
            }
            return employees;
        }
        public bool TryGetEmployeeByID(int id, out Employee employee)
        {
            Dbi511119Context context = new Dbi511119Context();
            employee = context.Employees.Where(e => e.Id == id).FirstOrDefault();
            if (employee != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}
