using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkSphere.Application.DTOs.Department;
using WorkSphere.Application.DTOs.Employees;
using WorkSphere.Application.DTOs.Position;
using WorkSphere.Application.Interfaces;
using WorkSphere.Domain.Entities;
using WorkSphere.Infrastructure.Persistence.Repositories;

namespace WorkSphere.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase

{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IPositionRepository _positionRepository;


    public EmployeesController(
    IEmployeeRepository employeeRepository,
    IDepartmentRepository departmentRepository,
    IPositionRepository positionRepository)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _positionRepository = positionRepository;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _employeeRepository.GetAllAsync();

        var response = employees.Select(employee => new EmployeeResponse
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Phone = employee.Phone,
            BirthDate = employee.BirthDate,
            HireDate = employee.HireDate,
            Salary = employee.Salary,

            Department = new DepartmentResponse
            {
                Id = employee.Department.Id,
                Name = employee.Department.Name,
                Description = employee.Department.Description
            },

            Position = new PositionResponse
            {
                Id = employee.Position.Id,
                Name = employee.Position.Name,
                Description = employee.Position.Description
            }

        }).ToList();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);


        if (employee == null)
            return NotFound();


        var response = new EmployeeResponse
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Phone = employee.Phone,
            BirthDate = employee.BirthDate,
            HireDate = employee.HireDate,
            Salary = employee.Salary,

            Department = new DepartmentResponse
            {
                Id = employee.Department.Id,
                Name = employee.Department.Name,
                Description = employee.Department.Description
            },

            Position = new PositionResponse
            {
                Id = employee.Position.Id,
                Name = employee.Position.Name,
                Description = employee.Position.Description
            }

        };

        return Ok(response);
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeRequest request)
    {

        var department = await _departmentRepository.GetByIdAsync(request.DepartmentId);
        var position = await _positionRepository.GetByIdAsync(request.PositionId);

        if (department == null)
        {
            return BadRequest("El departamento no existe.");
        }

        if (position == null)
        {
            return BadRequest("La posicion no existe.");
        }

        var employee = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            BirthDate = request.BirthDate,
            HireDate = request.HireDate,
            Salary = request.Salary,
            DepartmentId = request.DepartmentId,
            PositionId = request.PositionId
        };

        await _employeeRepository.AddAsync(employee);

        var response = new EmployeeResponse
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Phone = employee.Phone,
            BirthDate = employee.BirthDate,
            HireDate = employee.HireDate,
            Salary = employee.Salary,
            Department = new DepartmentResponse
            {
                Id = employee.Department!.Id,
                Name = employee.Department.Name,
                Description = employee.Department.Description
            },

            Position = new PositionResponse
            {
                Id = employee.PositionId,
                Name = employee.Position.Name,
                Description = employee.Position.Description
            },
        };

        return Ok(response);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateEmployeeRequest request)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
 

        if (employee == null)
            return NotFound();
        

        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.Email = request.Email;
        employee.Phone = request.Phone;
        employee.BirthDate = request.BirthDate; 
        employee.HireDate = request.HireDate;
        employee.Salary = request.Salary;
        employee.DepartmentId = request.DepartmentId;
        employee.PositionId = request.PositionId;


    await _employeeRepository.UpdateAsync(employee);

        var response = new EmployeeResponse
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Phone = employee.Phone,
            BirthDate = employee.BirthDate,
            HireDate = employee.HireDate,
            Salary = employee.Salary,
            Department = new DepartmentResponse
            {
                Id = employee.Department!.Id,
                Name = employee.Department.Name,
                Description = employee.Department.Description
            },
            Position = new PositionResponse
            {
                Id = employee.PositionId,
                Name = employee.Position.Name,
                Description = employee.Position.Description
            }
        };

        return Ok(response);
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);

        if (employee == null)
            return NotFound();

        await _employeeRepository.DeleteAsync(employee);

        return NoContent();
    }


}