using System;
using System.Collections.Generic;
using System.Text;

namespace WorkSphere.Application.DTOs.Employees;
public class UpdateEmployeeRequest
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public DateOnly BirthDate { get; set; }

    public DateOnly HireDate { get; set; }

    public decimal Salary { get; set; }

    public Guid DepartmentId { get; set; }

    public Guid PositionId { get; set; }
}
