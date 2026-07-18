using System;
using System.Collections.Generic;
using System.Text;

namespace WorkSphere.Application.DTOs.Department;

public class UpdateDepartmentRequest
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}