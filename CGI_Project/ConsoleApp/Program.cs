// See https://aka.ms/new-console-template for more information
using CGI_DAL.repositories;
using CGI_Models;

EmployeeRepository repository = new EmployeeRepository();
List<Employee> EMP = repository.GetEmployees(1);

foreach (Employee employee in EMP)
{
    Console.WriteLine(employee.FirstName + " " + employee.LastName);
}

Employee employee1;
repository.TryGetEmployeeByID(1,out employee1);
Console.WriteLine(employee1.FirstName + " " + employee1.LastName);

