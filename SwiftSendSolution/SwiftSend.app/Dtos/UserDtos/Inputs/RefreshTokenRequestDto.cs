using System.ComponentModel.DataAnnotations;

namespace SwiftSend.app.Dtos.UserDtos.Inputs
{
    public class RefreshTokenRequestDto
    {
        [Required]
        public string RefreshToken { get; set; }
        [Required]
        public string JWTToken { get; set; }
    }
}
