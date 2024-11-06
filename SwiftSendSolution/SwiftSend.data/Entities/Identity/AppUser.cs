using Microsoft.AspNetCore.Identity;

namespace SwiftSend.data.Entities.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
