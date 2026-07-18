using Microsoft.AspNetCore.Mvc;
using WorkSphere.Application.DTOs.Department;
using WorkSphere.Application.DTOs.Position;
using WorkSphere.Application.Interfaces;
using WorkSphere.Domain.Entities;

namespace WorkSphere.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PositionController : ControllerBase
{

    private readonly IPositionRepository _positionRepository;

    public PositionController(IPositionRepository positionRepository)
    {
        _positionRepository = positionRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var positions = await _positionRepository.GetAllAsync();

        var response = positions.Select(position => new PositionResponse
        {
            Id = position.Id,
            Name = position.Name,
            Description = position.Description
        });

        return Ok(response);
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreatePositionRequest request)
    {
        var position = new Position
        {
            Name = request.Name,
            Description = request.Description,

        };

        await _positionRepository.AddAsync(position);

        var response = new PositionResponse
        {
            Id = position.Id,
            Name = position.Name,
            Description = position.Description
        };

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdatePositionRequest request)
    {
        var postion = await _positionRepository.GetByIdAsync(id);

        if (postion == null)
            return NotFound();

        postion.Name = request.Name;
        postion.Description = request.Description;

        await _positionRepository.UpdateAsync(postion);

        var response = new PositionResponse
        {
            Id = postion.Id,
            Name = postion.Name,
            Description = postion.Description
        };

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var department = await _positionRepository.GetByIdAsync(id);

        if (department == null)
            return NotFound();

        await _positionRepository.DeleteAsync(department);

        return NoContent();
    }


}


