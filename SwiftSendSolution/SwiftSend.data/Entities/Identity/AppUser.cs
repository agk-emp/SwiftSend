using AspNetCore.Identity.Mongo.Model;

namespace SwiftSend.data.Entities.Identity
{
    public class AppUser : MongoUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
