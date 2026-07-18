using System;
using System.Collections.Generic;
using System.Text;
using WorkSphere.Application.Interfaces;
using WorkSphere.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WorkSphere.Infrastructure.Persistence.Repositories;
public class PositionRepository : IPositionRepository
{
    private readonly WorkSphereDbContext _context;

    public PositionRepository(WorkSphereDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Position position)
    {
        await _context.Positions.AddAsync(position);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Position position)
    {
        _context.Positions.Remove(position);
        await _context.SaveChangesAsync();
    }


    public async Task<List<Position>> GetAllAsync()
    {
        return await _context.Positions.ToListAsync();
    }

    public async Task<Position?> GetByIdAsync(Guid id)
    {
        return await _context.Positions.FindAsync(id);
    }

    public async Task UpdateAsync(Position position)
    {
        _context.Positions.Update(position);
        await _context.SaveChangesAsync();
    }

}

