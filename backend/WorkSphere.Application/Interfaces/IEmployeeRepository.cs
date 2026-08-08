using WorkSphere.Application.Common;
using WorkSphere.Application.DTOs.Employees;
using WorkSphere.Domain.Entities;

namespace WorkSphere.Application.Interfaces;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllAsync();

    Task<PagedData<Employee>> GetPagedAsync(EmployeeFilter filter);

    Task<Employee?> GetByIdAsync(Guid id);

    Task AddAsync(Employee employee);

    Task UpdateAsync(Employee employee);

    Task DeleteAsync(Employee employee);
}
