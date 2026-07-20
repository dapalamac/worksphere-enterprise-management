using WorkSphere.Application.DTOs.Position;
using WorkSphere.Application.Interfaces;
using WorkSphere.Domain.Entities;

namespace WorkSphere.Application.Services.Positions;

public class PositionService : IPositionService
{

    private readonly IPositionRepository _positionRepository;

    public PositionService(IPositionRepository positionRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task<PositionResponse> CreateAsync(CreatePositionRequest request)
    {
        var position = new Position
        {
            Name = request.Name,
            Description = request.Description,

        };

        await _positionRepository.AddAsync(position);

        return new PositionResponse
        {
            Id = position.Id,
            Name = position.Name,
            Description = position.Description
        };

    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var position = await _positionRepository.GetByIdAsync(id);

        if (position == null)
            return false;

        await _positionRepository.DeleteAsync(position);

        return true;
    }

    public async Task<List<PositionResponse>> GetAllAsync()
    {
        var positions = await _positionRepository.GetAllAsync();

        var response = positions.Select(position => new PositionResponse
        {
            Id = position.Id,
            Name = position.Name,
            Description = position.Description
        }).ToList();

        return response;
    }

    public async Task<PositionResponse?> GetByIdAsync(Guid id)
    {
        var position = await _positionRepository.GetByIdAsync(id);

        if (position == null)
            return null;

        return new PositionResponse
        {
            Id = position.Id,
            Name = position.Name,
            Description = position.Description,
        };
    }


    public async Task<PositionResponse?> UpdateAsync(Guid id, UpdatePositionRequest request)
    {
        var position = await _positionRepository.GetByIdAsync(id);

        if (position == null)
            return null;

        position.Name = request.Name;
        position.Description = request.Description;

        await _positionRepository.UpdateAsync(position);

        return new PositionResponse
        {
            Id = position.Id,
            Name = position.Name,
            Description = position.Description
        };

    }
}