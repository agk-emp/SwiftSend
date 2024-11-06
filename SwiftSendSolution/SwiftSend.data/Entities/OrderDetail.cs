using SwiftSend.data.Entities.Enums;

namespace SwiftSend.data.Entities
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int DeliveryPersonId { get; set; }
        public DeleiveryPerson DeleiveryPerson { get; set; }
        public int RestaurantId { get; set; }
        public string MealName { get; set; }
        public Meal Meal { get; set; }
        public OrderDetailStatus OrderDetailStatus { get; set; }

        public OrderDetail()
        {
            OrderDetailStatus = OrderDetailStatus.UnSeen;
        }


    }
}
