using System.ComponentModel.DataAnnotations;

namespace SwiftSend.app.Dtos.UserDtos.Inputs
{
    public class LoginDto
    {
        [Required]
        public string EmailOrUsername { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
