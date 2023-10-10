// See https://aka.ms/new-console-template for more information
using CGI_Project_WebApp_DAL.repositories;
using CGI_Project_WebApp_Models;

EmployeeRepository repository = new EmployeeRepository();

List<Employee> employees = new List<Employee>();

employees = repository.GetEmployees(1);

foreach (Employee employee in employees)
{
    Console.WriteLine(employee.FirstName + " "+ employee.LastName + " - " + employee.Company.Name);
}

List<Poll>polls = new List<Poll>();

repository.TryGetAllPollesWithSuggestionFromEmloyee(out polls, employees[0]);

repository.TryChangeEmployeeRoll(2, 1);

Employee emp;
repository.TryGetEmployeeByID(2, out emp);

repository.TryGetWinningPolls(out polls, employees[1]);



//repository.TryChangeEmployeeRoll()

int i = 0;
