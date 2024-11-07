using MongoDB.Bson.Serialization.Attributes;

namespace SwiftSend.data.Entities
{
    public class DeliveryPerson
    {
        [BsonElement("orderDetails")]
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
