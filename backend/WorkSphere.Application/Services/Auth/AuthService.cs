using WorkSphere.Application.Auth.DTOs;
using WorkSphere.Application.Exceptions;
using WorkSphere.Application.Interfaces;

namespace WorkSphere.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthService(
    IUserRepository userRepository,
    IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
        {
            throw new UnauthorizedException("Correo o contraseña incorrectos.");
        }

        var passwordIsValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!passwordIsValid)
            throw new UnauthorizedException("Correo o contraseña incorrectos.");

        return new LoginResponse
        {
            Token = _jwtService.GenerateToken(user)
        };
    }
}