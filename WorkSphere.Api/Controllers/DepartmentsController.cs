using Microsoft.AspNetCore.Mvc;
using WorkSphere.Application.DTOs.Department;
using WorkSphere.Application.DTOs.Employees;
using WorkSphere.Application.Interfaces;
using WorkSphere.Domain.Entities;
using WorkSphere.Infrastructure.Persistence.Repositories;

namespace WorkSphere.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class DepartmentController : ControllerBase
{

    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentController(IDepartmentRepository DepartmentRepository)
    {
        _departmentRepository = DepartmentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var departments = await _departmentRepository.GetAllAsync();

        var response = departments.Select(department => new DepartmentResponse
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description
        });

        return Ok(response);
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateDepartmentRequest request)
    {
        var department = new Department
        {
            Name = request.Name,
            Description = request.Description,
           
        };

        await _departmentRepository.AddAsync(department);

        var response = new DepartmentResponse
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description
        };

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateDepartmentRequest request)
    {
        var department = await _departmentRepository.GetByIdAsync(id);

        if (department == null)
            return NotFound();

        department.Name = request.Name;
        department.Description = request.Description;

        await _departmentRepository.UpdateAsync(department);

        var response = new DepartmentResponse
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description
        };

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var department = await _departmentRepository.GetByIdAsync(id);

        if (department == null)
            return NotFound();

        await _departmentRepository.DeleteAsync(department);

        return NoContent();
    }


}
