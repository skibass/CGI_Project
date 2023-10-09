// See https://aka.ms/new-console-template for more information
using CGI_DAL.repositories;
using CGI_Models;

EmployeeRepository repository = new EmployeeRepository();

List<Employee> employees = new List<Employee>();

employees = repository.GetEmployees(1);

foreach (Employee employee in employees)
{
    Console.WriteLine(employee.FirstName + " "+ employee.LastName + " - " + employee.Company.Name);
}
