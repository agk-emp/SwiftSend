using MongoDB.Bson.Serialization.Attributes;
using SwiftSend.data.Entities.SharedModels;

namespace SwiftSend.data.Entities
{
    public class Order : AbstradctModel
    {
        [BsonElement("customerId")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [BsonElement("orderDetails")]
        public List<OrderDetail> OrderDetails { get; set; }

        public Order()
        {
            OrderDetails = new();
        }
    }
}
