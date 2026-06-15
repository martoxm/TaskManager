using TaskManager.Application.DTOs;

namespace TaskManager.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Register(CreateUserDto dto);

        Task<AuthResponseDto> Login(LoginUserDto dto);
    }
}