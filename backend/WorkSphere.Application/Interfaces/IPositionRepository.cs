using System;
using System.Collections.Generic;
using System.Text;
using WorkSphere.Domain.Entities;

namespace WorkSphere.Application.Interfaces;
public interface IPositionRepository
{
    Task<List<Position>> GetAllAsync();

    Task<Position?> GetByIdAsync(Guid id);

    Task AddAsync(Position position);

    Task UpdateAsync(Position position);

    Task DeleteAsync(Position position);


}

