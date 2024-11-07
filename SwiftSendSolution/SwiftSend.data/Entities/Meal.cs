using MongoDB.Bson.Serialization.Attributes;

namespace SwiftSend.data.Entities
{
    public class Meal
    {
        [BsonElement("restaurantId")]
        public int RestaurantId { get; set; }

        [BsonElement("mealName")]
        public string MealName { get; set; }
        public Restaurant Restaurant { get; set; }

        [BsonElement("orderDetails")]
        public List<OrderDetail> OrderDetails { get; set; }

        public Meal()
        {
            OrderDetails = new();
        }
    }
}
