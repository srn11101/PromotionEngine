using System.Collections.Generic;

namespace PromotionEngine.Models
{
    public class Promotion
    {
        public Promotion()
        {

        }
        public int PromotionID { get; set; }
        public Dictionary<string, int> ProductInfo { get; set; }
        public decimal PromoPrice { get; set; }

        public Promotion(int promoID, Dictionary<string, int> productInfo, decimal promoPrice)
        {
            this.PromotionID = promoID;
            this.ProductInfo = productInfo;
            this.PromoPrice = promoPrice;
        }
    }
}
