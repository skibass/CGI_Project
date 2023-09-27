// See https://aka.ms/new-console-template for more information
using CGI_DAL.Database_Models;

Dbi511119Context context = new Dbi511119Context();

Company testCompany = new Company();
testCompany.Name = Console.ReadLine();

context.Companies.Add(testCompany);
context.SaveChanges();

foreach (Company item in context.Companies)
{
    Console.WriteLine(item.Name);
}

