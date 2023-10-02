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
        public bool TryChangeEmployeeRoll(Role role, Employee employee)
        {
            return TryChangeEmployeeRoll(role.Id, employee.Id);
        }
        public bool TryChangeEmployeeRoll(int roleId, int employeeId)
        {
            Dbi511119Context context = new Dbi511119Context();

            Employee? employee = context.Employees.Where(emp => emp.Id == employeeId).FirstOrDefault();
            Role? role = context.Roles.Where(role => role.Id == roleId).FirstOrDefault();

            if (employee != null && role != null)
            {
                employee.Role = role;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

            

        }

        
        //moeten wij de werknemers kunnen maken of gaat dit via de 3de party tool?
        
    }
}
