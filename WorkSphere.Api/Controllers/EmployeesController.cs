using Microsoft.AspNetCore.Mvc;
using WorkSphere.Application.DTOs.Employees;
using WorkSphere.Application.Interfaces;

namespace WorkSphere.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase

{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _employeeService.GetAllAsync();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _employeeService.GetByIdAsync(id);

        return Ok(response);
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeRequest request)
    {
        var response = await _employeeService.CreateAsync(request);
        return Ok(response);

    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateEmployeeRequest request)
    {
        var response = await _employeeService.UpdateAsync(id, request);

        if (response == null)
            return NotFound();

        return Ok(response);
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _employeeService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }


}
