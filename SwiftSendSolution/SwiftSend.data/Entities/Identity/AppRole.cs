using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;

namespace SwiftSend.data.Entities.Identity
{
    public class AppRole : MongoIdentityRole<string>
    {
        public AppRole()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}
