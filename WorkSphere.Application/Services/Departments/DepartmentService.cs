using WorkSphere.Application.DTOs.Department;
using WorkSphere.Application.Interfaces;
using WorkSphere.Domain.Entities;

namespace WorkSphere.Application.Services.Departments;

public class DepartmentService : IDepartmentService

{

    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<DepartmentResponse> CreateAsync(CreateDepartmentRequest request)
    {
        var department = new Department
        {
            Name = request.Name,
            Description = request.Description
        };

        await _departmentRepository.AddAsync(department);

        return new DepartmentResponse
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var department = await _departmentRepository.GetByIdAsync(id);

        if (department == null)
            return false;

        await _departmentRepository.DeleteAsync(department);

        return true; 
    }

    public async Task<List<DepartmentResponse>> GetAllAsync()
    {
        var departments = await _departmentRepository.GetAllAsync();

        var response = departments.Select(department => new DepartmentResponse
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description
        }).ToList();

        return response;
    }

    public async Task<DepartmentResponse?> GetByIdAsync(Guid id)
    {
        var department = await _departmentRepository.GetByIdAsync(id);

        if (department == null)
            return null;

        return new DepartmentResponse
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
        };
    }

    public async Task<DepartmentResponse?> UpdateAsync(Guid id, UpdateDepartmentRequest request)
    {
        var department = await _departmentRepository.GetByIdAsync(id);

        if (department == null)
            return null;

        department.Name = request.Name;
        department.Description = request.Description;

        await _departmentRepository.UpdateAsync(department);

        return new DepartmentResponse
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description
        };
    }
}
