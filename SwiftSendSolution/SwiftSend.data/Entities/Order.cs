using SwiftSend.data.Entities.SharedModels;

namespace SwiftSend.data.Entities
{
    public class Order : AbstradctModel
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
