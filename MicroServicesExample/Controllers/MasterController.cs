using MicroServicesExample.Models;
using MicroServicesExample.Proxies;
using Microsoft.AspNetCore.Mvc;

namespace MicroServicesExample.Controllers;

[ApiController]
[Route("[controller]")]

public class MasterController : ControllerBase
{
    private readonly EmployerProxy Employer_API;
    private readonly UserProxy User_API;
    public MasterController(EmployerProxy Employer_API, UserProxy User_API)
    {
        this.Employer_API = Employer_API;
        this.User_API = User_API;
    }

    [HttpGet]
    [Route("GetUserById")]
    public async Task<IActionResult> GetUserById(string Email)
    {
        Console.WriteLine(Email);
        var user = await User_API.GetUser(Email);
        return Ok(user);
    }

    [HttpGet]
    [Route("GetAllEmployees")]
    public async Task<IActionResult> GetAllEmployees()
    {
        Console.WriteLine("Email");
        var user = await Employer_API.GetAllUser();
        return Ok(user);
    }

    [HttpPost]
    [Route("AddUser")]
    public async Task<IActionResult> AddUser(AddUserDTO user)
    {
        Console.WriteLine(user.Email);
        var employerTask = Employer_API.AddUser(user);
        var userTask = User_API.AddUser(user);
        await Task.WhenAll(employerTask, userTask);
        return Ok();
    }

    [HttpDelete]
    [Route("DeleteEmployee")]

    public async Task<IActionResult> DeleteEmployee(string Email)
    {
        Console.WriteLine(Email);
        var employeeTask = Employer_API.DeleteEmployee(Email);
        var userTask = User_API.DeleteUser(Email);
        await Task.WhenAll(employeeTask, userTask);
        return Ok();
    }

    [HttpPut]
    [Route("UpdatePassword")]
    public async Task<IActionResult> UpdatePassword(string Email, string Password)
    {
        Console.WriteLine(Email);
        var user = await User_API.UpdatePassword(Email, Password);
        return Ok(user);
    }

}
