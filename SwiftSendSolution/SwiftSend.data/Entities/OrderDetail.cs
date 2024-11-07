using MongoDB.Bson.Serialization.Attributes;
using SwiftSend.data.Entities.Enums;

namespace SwiftSend.data.Entities
{
    public class OrderDetail
    {
        [BsonElement("orderId")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [BsonElement("deliveryPersonId")]
        public int DeliveryPersonId { get; set; }
        public DeliveryPerson DeleiveryPerson { get; set; }
        [BsonElement("restaurantId")]
        public int RestaurantId { get; set; }
        [BsonElement("mealName")]
        public string MealName { get; set; }
        public Meal Meal { get; set; }
        [BsonElement("orderDetailStatus")]
        public OrderDetailStatus OrderDetailStatus { get; set; }

        public OrderDetail()
        {
            OrderDetailStatus = OrderDetailStatus.UnSeen;
        }


    }
}
