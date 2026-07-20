using WorkSphere.Application.DTOs.Department;
using WorkSphere.Application.DTOs.Employees;
using WorkSphere.Application.DTOs.Position;
using WorkSphere.Application.Interfaces;
using WorkSphere.Domain.Entities;

namespace WorkSphere.Application.Services.Employees;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IPositionRepository _positionRepository;


    public EmployeeService(
    IEmployeeRepository employeeRepository,
    IDepartmentRepository departmentRepository,
    IPositionRepository positionRepository)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _positionRepository = positionRepository;
    }


    public async Task<EmployeeResponse?> CreateAsync(CreateEmployeeRequest request)
    {
        var department = await _departmentRepository.GetByIdAsync(request.DepartmentId);

        if (department == null)
            return null;


        var position = await _positionRepository.GetByIdAsync(request.PositionId);

        if (position == null)
            return null;


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

        employee.Department = department;
        employee.Position = position;

        await _employeeRepository.AddAsync(employee);

        return MapToResponse(employee);

    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);

        if (employee == null)
            return false;

        await _employeeRepository.DeleteAsync(employee);

        return true;
    }

    public async Task<List<EmployeeResponse>> GetAllAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();

        var response = employees.Select(MapToResponse).ToList();

        return response;
    }

    public async Task<EmployeeResponse?> GetByIdAsync(Guid id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);

        if (employee == null)
            return null;

        return MapToResponse(employee);

    }

    public async Task<EmployeeResponse?> UpdateAsync(Guid id, UpdateEmployeeRequest request)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);

        if (employee == null)
            return null;

        var department = await _departmentRepository.GetByIdAsync(request.DepartmentId);

        if (department == null)
            throw new Exception("Departamento no encontrado");

        var position = await _positionRepository.GetByIdAsync(request.PositionId);

        if (position == null)
            throw new Exception("Position no encontrada");


        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.Email = request.Email;
        employee.Phone = request.Phone;
        employee.BirthDate = request.BirthDate;
        employee.HireDate = request.HireDate;
        employee.Salary = request.Salary;
        employee.DepartmentId = request.DepartmentId;
        employee.PositionId = request.PositionId;
        employee.Department = department;
        employee.Position = position;

        await _employeeRepository.UpdateAsync(employee);

        return MapToResponse(employee);

    }

    private static DepartmentResponse MapDepartment(Department department)
    {
        return new DepartmentResponse
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description
        };
    }

    private static PositionResponse MapPosition(Position position)
    {
        return new PositionResponse
        {
            Id = position.Id,
            Name = position.Name,
            Description = position.Description
        };
    }

    private static EmployeeResponse MapToResponse(Employee employee)
    {
        return new EmployeeResponse
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Phone = employee.Phone,
            BirthDate = employee.BirthDate,
            HireDate = employee.HireDate,
            Salary = employee.Salary,


            Department = MapDepartment(employee.Department),

            Position = MapPosition(employee.Position)
        };
    }
}
