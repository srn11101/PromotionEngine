using MediatR;
using PromotionEngine.Models;
using System.Collections.Generic;

namespace PromotionEngine.Business.Requests
{
    public class OrderPromotionRequest:IRequest<List<ProductPriceResponse>>
    {
        public List<Order> Orders { get; set; }
        public List<Promotion> Promotions { get; set; }
    }
}
