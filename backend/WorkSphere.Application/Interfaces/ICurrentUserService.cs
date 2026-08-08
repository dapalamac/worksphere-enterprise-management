namespace WorkSphere.Application.Interfaces;

public interface ICurrentUserService
{
    Guid UserId { get; }

    string Email { get; }

    string Name { get; }

    string Role { get; }
}
