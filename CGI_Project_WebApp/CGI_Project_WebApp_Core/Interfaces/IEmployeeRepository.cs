using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Core.Interfaces
{
    public interface IEmployeeRepository
    {
        public List<Employee> GetEmployees(int companyId);
        public List<Employee> GetEmployees();
        public bool TryGetEmployeeByEmail(string email, out Employee employee);
        public bool TryGetEmployeeByID(int id, out Employee employee);
        public bool TryGetEmployeeByEmailAndFirstName(string Email, string FirstName, out Employee employee);
        public bool TryChangeEmployeeRoll(Role role, Employee employee);
        public List<PollSuggestion> GetPollSuggestionsByEmployeeId(Employee employee);
        public List<PollSuggestion> GetPollSuggestionsByEmployeeId(int employeeId);
        public bool TryAddEmployee(Employee emp);

    }
}
