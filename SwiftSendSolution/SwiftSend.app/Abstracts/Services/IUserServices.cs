using SwiftSend.app.Dtos.UserDtos.Inputs;
using SwiftSend.app.Dtos.UserDtos.Outputs;

namespace SwiftSend.app.Abstracts.Services
{
    public interface IUserServices
    {
        Task<RefreshTokenResultDto> Register(RegisterDto registerDto);
        Task<RefreshTokenResultDto> Login(LoginDto loginDto);
        Task<RefreshTokenResultDto> RefreshTokenForUser(RefreshTokenRequestDto refreshTokenRequestDto);
    }
}
