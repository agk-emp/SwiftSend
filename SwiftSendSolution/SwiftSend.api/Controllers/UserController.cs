using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwiftSend.app.Abstracts.Services;
using SwiftSend.app.Dtos.UserDtos.Inputs;

namespace SwiftSend.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [AllowAnonymous]
    public class UserController : ControllerBase //Todo: create a common base controller to register most frequent services
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
        {
            try
            {
                var result = await _userServices.Register(registerDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to register {ex.Message}");
            }
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
        {
            try
            {
                var result = await _userServices.Login(loginDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to login {ex.Message}");

            }
        }

        [HttpPost(nameof(RefreshUserToken))]
        public async Task<IActionResult> RefreshUserToken([FromBody] RefreshTokenRequestDto refreshTokenRequestDto)
        {
            try
            {
                var result = await _userServices.RefreshTokenForUser(refreshTokenRequestDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to refresh token {ex.Message}");
            }
        }
    }
}
