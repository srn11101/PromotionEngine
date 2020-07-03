using PromotionEngine.Business.Interface;
using PromotionEngine.Models;
using System.Linq;

namespace PromotionEngine.Business.Implementations
{
    public class Promotion1: IPromotion
    {
        //returns PromotionID and count of promotions
        public decimal GetTotalPrice(Order order, Promotion promotion)
        {
            decimal d = 0M;
            //get count of promoted products in order
            var copp = order.Products
                .GroupBy(x => x.Id)
                .Where(grp => promotion.ProductInfo.Any(y => grp.Key == y.Key && grp.Count() >= y.Value))
                .Select(grp => grp.Count())
                .Sum();
            //get count of promoted products from promotion
            int promotedProductCount = promotion.ProductInfo.Sum(kvp => kvp.Value);
            while (copp >= promotedProductCount)
            {
                d += promotion.PromoPrice;
                copp -= promotedProductCount;
            }
            return d;
        }
    }
}
