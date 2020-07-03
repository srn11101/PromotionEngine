
namespace PromotionEngine.Models
{
    public class ProductPriceResponse
    {
        public int OrderID { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalPrice { get; set; }
       
    }
}
