using WorkSphere.Domain.Enums;

namespace WorkSphere.Application.DTOs.Employees;

public class EmployeeFilter
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string? Search { get; set; }

    public Guid? DepartmentId { get; set; }

    public Guid? PositionId { get; set; }

    public string? SortBy { get; set; }

    public SortDirection SortDirection { get; set; } = SortDirection.Asc;

}
