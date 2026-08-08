using System;
using System.Collections.Generic;
using System.Text;
using WorkSphere.Application.DTOs.Department;
using WorkSphere.Application.DTOs.Position;

namespace WorkSphere.Application.DTOs.Employees;

public class EmployeeResponse
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public DateOnly BirthDate { get; set; }

    public DateOnly HireDate { get; set; }

    public decimal Salary { get; set; }

    public DepartmentResponse? Department { get; set; } = new();

    public PositionResponse? Position { get; set; } = new();
}
