using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkSphere.Application.Common;
using WorkSphere.Application.DTOs.Auth;
using WorkSphere.Application.DTOs.Employees;
using WorkSphere.Application.Interfaces;


namespace WorkSphere.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase

{
    private readonly IEmployeeService _employeeService;
    private readonly ICurrentUserService _currentUser;

    public EmployeesController(
    IEmployeeService employeeService,
    ICurrentUserService currentUser)
    {
        _employeeService = employeeService;
        _currentUser = currentUser;
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        return Ok(new CurrentUserResponse
        {
            Id = _currentUser.UserId.ToString(),
            Name = _currentUser.Name,
            Email = _currentUser.Email,
            Role = _currentUser.Role
        });
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<EmployeeResponse>>>> GetAll(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
    {
        var employees = await _employeeService.GetPagedAsync(page, pageSize);

        return Ok(employees);
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _employeeService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }




}
