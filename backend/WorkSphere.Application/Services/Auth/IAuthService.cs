using WorkSphere.Application.Auth.DTOs;

namespace WorkSphere.Application.Services.Auth;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
}