using EmployeeDetails.Models;
using EmployeeDetails.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDetails.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeesRepository _repository;

    public EmployeesController(IEmployeesRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Route("EmployeesList")]
    public IActionResult GetEmployeesList()
    {
        return Ok(_repository.GetEmployees());
    }

    [HttpPost]
    [Route("AddEmployee")]
    public IActionResult AddEmployee(Employee employee)
    {
        _repository.AddEmployee(employee);
        return Ok();
    }

    [HttpDelete]
    [Route("DeleteEmployee")]
    public IActionResult DeleteEmployee(string Email)
    {
        _repository.DeleteEmployee(Email);
        return Ok();
    }

    [HttpPut]
    [Route("UpdateEmployee")]
    public IActionResult UpdateEmployee(Employee employee)
    {
        _repository.UpdateEmployee(employee);
        return Ok();
    }
}