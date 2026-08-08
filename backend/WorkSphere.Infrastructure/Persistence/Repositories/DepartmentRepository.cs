using System;
using System.Collections.Generic;
using System.Text;
using WorkSphere.Application.Interfaces;
using WorkSphere.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WorkSphere.Infrastructure.Persistence.Repositories;
public class DepartmentRepository : IDepartmentRepository
{
    private readonly WorkSphereDbContext _context;

    public DepartmentRepository(WorkSphereDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Department department)
    {
        await _context.Departments.AddAsync(department);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Department department)
    {
        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Department>> GetAllAsync()
    {
        return await _context.Departments.ToListAsync();
    }

    public async Task<Department?> GetByIdAsync(Guid id)
    {
        return await _context.Departments.FindAsync(id);
    }

    public async Task UpdateAsync(Department department)
    {
        _context.Departments.Update(department);
        await _context.SaveChangesAsync();
    }
}
