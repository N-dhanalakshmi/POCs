using EmployeeDetails.Models;

namespace EmployeeDetails.Repositories;

public interface IEmployeesRepository{
    IEnumerable<Employee>  GetEmployees();
    void AddEmployee(Employee employee);
    void DeleteEmployee(string Email);
    void UpdateEmployee(Employee employee);

}