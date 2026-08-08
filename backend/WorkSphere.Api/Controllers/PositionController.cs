using Microsoft.AspNetCore.Mvc;
using WorkSphere.Application.DTOs.Position;
using WorkSphere.Application.Interfaces;

namespace WorkSphere.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PositionController : ControllerBase
{

    private readonly IPositionService _positionService;

    public PositionController(IPositionService positionService)
    {
        _positionService = positionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _positionService.GetAllAsync();

        return Ok(response);
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreatePositionRequest request)
    {
        var response = await _positionService.CreateAsync(request);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdatePositionRequest request)
    {
        var response = await _positionService.UpdateAsync(id, request);

        if (response == null)
            return NotFound();

        return Ok(response);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _positionService.GetByIdAsync(id);

        if (response == null)
            return NotFound();

        return Ok(response);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var delete = await _positionService.DeleteAsync(id);

        if (!delete)
            return NotFound();

        return NoContent();
    }


}


