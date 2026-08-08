using System;
using System.Collections.Generic;
using System.Text;

namespace WorkSphere.Application.DTOs.Department;
public class DepartmentResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

}
