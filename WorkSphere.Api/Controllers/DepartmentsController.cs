using Hangfire;
using Microsoft.AspNetCore.Mvc;
using WorkSphere.Application.DTOs.Department;
using WorkSphere.Application.Interfaces;

namespace WorkSphere.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class DepartmentController : ControllerBase
{

    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpPost("test-job")]
    public IActionResult TestJob()
    {
        // Agregar un trabajo en segundo plano usando Hangfire
        BackgroundJob.Enqueue<INotificationService>(
            service => service.SendWelcomeEmail(1));

        return Ok("Job agregado correctamente.");
    }

    //[HttpPost("test-delayed-job")]
    //public IActionResult TestDelayedJob()
    //{
    //    BackgroundJob.Schedule<INotificationService>(
    //        service => service.SendWelcomeEmail(1),
    //        TimeSpan.FromMinutes(1));

    //    return Ok("Job programado correctamente.");
    //}

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _departmentService.GetAllAsync();

        return Ok(response);
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateDepartmentRequest request)
    {
        var response = await _departmentService.CreateAsync(request);

        return Ok(response);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _departmentService.GetByIdAsync(id);

        if (response == null)
            return NotFound();

        return Ok(response);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateDepartmentRequest request)
    {
        var response = await _departmentService.UpdateAsync(id, request);

        if (response == null)
            return NotFound();

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _departmentService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }




}
