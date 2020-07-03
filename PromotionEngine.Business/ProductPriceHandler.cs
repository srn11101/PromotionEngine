using MediatR;
using PromotionEngine.Business.Interface;
using PromotionEngine.Business.Requests;
using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static PromotionEngine.Models.Enums;

namespace PromotionEngine.Business
{
    public class ProductPriceHandler : IRequestHandler<OrderPromotionRequest, List<ProductPriceResponse>>
    {
        private readonly IPromotion _promotion;
        public ProductPriceHandler(Func<PromotionTypes, IPromotion> promotions)
        {
            _promotion = promotions(PromotionTypes.Promotion1);
        }
        public async Task<List<ProductPriceResponse>> Handle(OrderPromotionRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            List<ProductPriceResponse> productPriceResponses = new List<ProductPriceResponse>();
            foreach (Order ord in request.Orders)
            {
                List<decimal> promoprices = request.Promotions
                    .Select(promo => _promotion.GetTotalPrice(ord, promo))
                    .ToList();
                decimal origprice = ord.Products.Sum(x => x.Price);
                decimal promoprice = promoprices.Sum();
                productPriceResponses.Add(new ProductPriceResponse { OrderID = ord.OrderID, OriginalPrice = origprice, Discount = promoprice, FinalPrice = origprice - promoprice });
            }
            return productPriceResponses;
        }
    }
}
