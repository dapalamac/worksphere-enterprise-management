using WorkSphere.Domain.Entities;

namespace WorkSphere.Application.Services.Auth;

public interface IJwtService
{
    string GenerateToken(User user);
}
