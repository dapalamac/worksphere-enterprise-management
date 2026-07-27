using Microsoft.EntityFrameworkCore;
using WorkSphere.Application.Common;
using WorkSphere.Application.DTOs.Employees;
using WorkSphere.Application.Interfaces;
using WorkSphere.Domain.Entities;
using WorkSphere.Domain.Enums;

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

    public async Task<PagedData<Employee>> GetPagedAsync(EmployeeFilter filter)
    {

        var query = _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Position)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            string search = filter.Search.Trim();
            query = query.Where(e => (e.FirstName + " " + e.LastName).Contains(search));
        }

        if (filter.DepartmentId.HasValue)
        {
            query = query.Where(e => e.DepartmentId == filter.DepartmentId);
        }

        if (filter.PositionId.HasValue)
        {
            query = query.Where(e => e.PositionId == filter.PositionId);
        }

        if (filter.SortBy == "FirstName")
        {

            if (filter.SortDirection == SortDirection.asc)
            {
                query = query.OrderBy(e => e.FirstName);
            }
            else
            {
                query = query.OrderByDescending(e => e.FirstName);
            }
        }

        else if (filter.SortBy == "LastName")
        {
            if (filter.SortDirection == SortDirection.asc)
            {
                query = query.OrderBy(e => e.LastName);
            }
            else
            {
                query = query.OrderByDescending(e => e.LastName);
            }
        }

        else if (filter.SortBy == "CreatedAt")
        {
            if (filter.SortDirection == SortDirection.asc)
            {
                query = query.OrderBy(e => e.CreatedAt);
            }
            else
            {
                query = query.OrderByDescending(e => e.CreatedAt);
            }
        }

        var totalRecords = await query.CountAsync();


        var employees = await query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new PagedData<Employee>
        {
            Items = employees,
            TotalRecords = totalRecords
        };


    }
}