using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SwiftSend.data.Entities.Identity
{
    public class AppUser : MongoIdentityUser<string>
    {

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        public AppUser()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}
