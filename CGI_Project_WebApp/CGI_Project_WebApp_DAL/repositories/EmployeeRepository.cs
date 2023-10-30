using CGI_Project_WebApp_DAL.Database_Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using CGI_Project_WebApp_Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace CGI_Project_WebApp_DAL.repositories
{
    public class EmployeeRepository
    {
        public List<Employee> GetEmployees(int companyId = 1)
        {
            Dbi511119Context context = new Dbi511119Context();
            List<Employee> employees = context.Employees.Include(e => e.Company).Include(e => e.Role).Include(e => e.Suggestions).Include(e => e.Votes).Where(e => e.CompanyId == companyId).ToList();//
            if (employees == null)
            {
                employees = new List<Employee>();
            }
            return employees;
        }
        public bool TryGetEmployeeByID(int id, out Employee employee)
        {
            Dbi511119Context context = new Dbi511119Context();
            employee = context.Employees.Include(e => e.Company).Include(e => e.Role).Where(e => e.Id == id).FirstOrDefault();
            if (employee != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryGetEmployeeByEmailAndFirstName(string Email, string FirstName, out Employee employee)
        {
            Dbi511119Context context = new Dbi511119Context();
            employee = context.Employees.Include(e => e.Company).Include(e => e.Role).Where(e => e.FirstName== FirstName && e.Email == Email).FirstOrDefault();
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
            Dbi511119Context context = new Dbi511119Context();
            try
            {
                context.Attach(employee);
                context.Attach(role);
                employee.Role = role;
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
         public List<PollSuggestion> GetPollSuggestionsByEmployeeId(Employee employee)
        {
            return GetPollSuggestionsByEmployeeId(employee.Id);

            
        }
        public List<PollSuggestion> GetPollSuggestionsByEmployeeId(int employeeId)
        {
            Dbi511119Context context = new Dbi511119Context();
            List<PollSuggestion> pollSuggestion = context.PollSuggestions.Include(e => e.Poll).Include(e => e.Suggestion).ThenInclude(s => s.Votes).Where(suggestion => suggestion.Suggestion.EmployeeId == employeeId).ToList();
            return pollSuggestion;


        }
        public bool TryAddEmployee(Employee emp)
        {
            try
            {
                Dbi511119Context DB = new Dbi511119Context();
                DB.Employees.Add(emp);
                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }




    //moeten wij de werknemers kunnen maken of gaat dit via de 3de party tool?

}

