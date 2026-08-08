using WorkSphere.Application.DTOs.Department;

namespace WorkSphere.Application.Interfaces;

public interface IDepartmentService
{
    Task<List<DepartmentResponse>> GetAllAsync();

    Task<DepartmentResponse?> GetByIdAsync(Guid id);

    Task<DepartmentResponse> CreateAsync(CreateDepartmentRequest request);

    Task<DepartmentResponse?> UpdateAsync(Guid id, UpdateDepartmentRequest request);

    Task<bool> DeleteAsync(Guid id);
}