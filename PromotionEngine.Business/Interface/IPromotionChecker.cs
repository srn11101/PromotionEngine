
using PromotionEngine.Models;

namespace PromotionEngine.Business.Interface
{
    public interface IPromotion
    {
        decimal GetTotalPrice(Order order, Promotion promotion);
    }
}
