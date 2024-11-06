namespace SwiftSend.data.Entities
{
    public class Meal
    {
        public int RestaurantId { get; set; }
        public string MealName { get; set; }
        public Restaurant Restaurant { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        public Meal()
        {
            OrderDetails = new();
        }
    }
}
