using MongoDB.Bson.Serialization.Attributes;
using SwiftSend.data.Entities.Identity;

namespace SwiftSend.data.Entities
{
    public class Customer : AppUser
    {
        [BsonElement("orders")]
        public List<Order> Orders { get; set; }
        public Customer()
        {
            Orders = new();
        }
    }
}
