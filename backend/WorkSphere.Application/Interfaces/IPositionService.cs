using System;
using System.Collections.Generic;
using System.Text;
using WorkSphere.Application.DTOs.Employees;
using WorkSphere.Application.DTOs.Position;

namespace WorkSphere.Application.Interfaces;

public interface IPositionService
{
    Task<List<PositionResponse>> GetAllAsync();

    Task<PositionResponse?> GetByIdAsync(Guid id);

    Task<PositionResponse> CreateAsync(CreatePositionRequest request);

    Task<PositionResponse?> UpdateAsync(Guid id, UpdatePositionRequest request);

    Task<bool> DeleteAsync(Guid id);
}
