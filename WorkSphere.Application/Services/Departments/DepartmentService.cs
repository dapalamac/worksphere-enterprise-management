using WorkSphere.Application.DTOs.Department;
using WorkSphere.Application.Interfaces;
using WorkSphere.Domain.Entities;

namespace WorkSphere.Application.Services.Departments;

public class DepartmentService : IDepartmentService

{

    private readonly IDepartmentRepository _departmentRepository;
    private readonly ICacheService _cache;

    public DepartmentService(IDepartmentRepository departmentRepository, ICacheService cache)
    {
        _departmentRepository = departmentRepository;
        _cache = cache;
    }

    public async Task<DepartmentResponse> CreateAsync(CreateDepartmentRequest request)
    {
        var department = new Department
        {
            Name = request.Name,
            Description = request.Description
        };

        await _departmentRepository.AddAsync(department);

        await _cache.RemoveAsync("departments");

        return MapToResponse(department);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var department = await _departmentRepository.GetByIdAsync(id);

        if (department == null)
            return false;

        await _departmentRepository.DeleteAsync(department);

        await _cache.RemoveAsync("departments");

        return true;
    }

    public async Task<List<DepartmentResponse>> GetAllAsync()
    {
        var cachedDepartments = await _cache.GetAsync<List<DepartmentResponse>>("departments");

        if (cachedDepartments != null)
            return cachedDepartments;

        var departments = await _departmentRepository.GetAllAsync();

        var response = departments.Select(MapToResponse).ToList();

        // Cache the departments list in Redis for 10 minutes
        await _cache.SetAsync("departments", response);

        return response;


    }

    public async Task<DepartmentResponse?> GetByIdAsync(Guid id)
    {
        var department = await _departmentRepository.GetByIdAsync(id);

        if (department == null)
            return null;

        return MapToResponse(department);
    }

    public async Task<DepartmentResponse?> UpdateAsync(Guid id, UpdateDepartmentRequest request)
    {
        var department = await _departmentRepository.GetByIdAsync(id);

        if (department == null)
            return null;

        department.Name = request.Name;
        department.Description = request.Description;

        await _departmentRepository.UpdateAsync(department);

        await _cache.RemoveAsync("departments");

        return MapToResponse(department);
    }


    private static DepartmentResponse MapToResponse(Department department)
    {
        return new DepartmentResponse
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description
        };
    }
}
