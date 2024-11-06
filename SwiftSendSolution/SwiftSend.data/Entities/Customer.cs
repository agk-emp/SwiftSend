using SwiftSend.data.Entities.Identity;

namespace SwiftSend.data.Entities
{
    public class Customer : AppUser
    {
        public List<Order> Orders { get; set; }
        public Customer()
        {
            Orders = new();
        }
    }
}
