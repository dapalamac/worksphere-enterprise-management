using WorkSphere.Application.Common;
using WorkSphere.Application.DTOs.Employees;

namespace WorkSphere.Application.Interfaces;

public interface IEmployeeService
{
    Task<List<EmployeeResponse>> GetAllAsync();

    Task<ApiResponse<PagedResult<EmployeeResponse>>> GetPagedAsync(int page, int pageSize, string? search, Guid? departmentId, Guid? positionId);

    Task<EmployeeResponse?> GetByIdAsync(Guid id);

    Task<EmployeeResponse?> CreateAsync(CreateEmployeeRequest request);

    Task<EmployeeResponse?> UpdateAsync(Guid id, UpdateEmployeeRequest request);

    Task<bool> DeleteAsync(Guid id);
}