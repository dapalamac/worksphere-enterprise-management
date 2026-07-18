using Microsoft.EntityFrameworkCore;
using WorkSphere.Application.Interfaces;
using WorkSphere.Domain.Entities;
using WorkSphere.Infrastructure.Persistence;

namespace WorkSphere.Infrastructure.Persistence.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly WorkSphereDbContext _context;

    public EmployeeRepository(WorkSphereDbContext context)
    {
        _context = context;
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Position)
            .ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        return await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Position)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task AddAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Employee employee)
    {
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }


}