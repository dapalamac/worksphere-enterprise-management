using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WorkSphere.Application.Interfaces;

namespace WorkSphere.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public Guid UserId =>
        Guid.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public string Email =>
        User?.FindFirstValue(ClaimTypes.Email)!;

    public string Name =>
        User?.FindFirstValue(ClaimTypes.Name)!;

    public string Role =>
        User?.FindFirstValue(ClaimTypes.Role)!;
}