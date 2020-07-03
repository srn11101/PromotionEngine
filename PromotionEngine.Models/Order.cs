using System.Collections.Generic;

namespace PromotionEngine.Models
{
    public class Order
    {
        public Order()
        {

        }
        public int OrderID { get; set; }
        public List<Product> Products { get; set; }

        public Order(int orderId, List<Product> productId)
        {
            this.OrderID = orderId;
            this.Products = productId;
        }
    }
}
