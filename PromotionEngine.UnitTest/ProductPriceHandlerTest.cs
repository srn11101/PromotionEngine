using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSubstitute;
using PromotionEngine.Business;
using PromotionEngine.Business.Interface;
using PromotionEngine.Business.Requests;
using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static PromotionEngine.Models.Enums;

namespace PromotionEngine.UnitTest
{
    public class ProductPriceHandlerTest
    {
        private ILogger _logger;
        private List<ProductPriceResponse> _testModel;
        private OrderPromotionRequest _request;
        private List<ProductPriceResponse> _response;
        private IPromotion _promotion;
        private Func<PromotionTypes, IPromotion> _delegatePromotionType;
        public ProductPriceHandlerTest()
        {
            _logger = Substitute.For<ILogger>();
            _promotion = Substitute.For<IPromotion>();
            _delegatePromotionType = Substitute.For<Func<PromotionTypes, IPromotion>>();
            _delegatePromotionType.Invoke(PromotionTypes.Promotion1);
        }

        [Theory]
        [InlineData("PromtionHandlerMockRequest.json", "PromotionHandlerMockResponse.json")]
        public async Task ProductPriceHandlerTest_Check_FinalPrice(string requestFileName, string responseFileName)
        {
            
            var requestData = System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "Request", requestFileName));
            _request = JsonConvert.DeserializeObject<OrderPromotionRequest>(requestData);
            var responseData = System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "Response", responseFileName));
            _response = JsonConvert.DeserializeObject<List<ProductPriceResponse>>(responseData);
            Order order = new Order();
            Promotion promotion = new Promotion();
            _promotion.GetTotalPrice(order,promotion).Returns(130);
            var handler = Substitute.For<ProductPriceHandler>(_delegatePromotionType);
            var result = await handler.Handle(_request, new CancellationToken());
            Assert.Equal(result[0].FinalPrice,_response[0].FinalPrice);
        }

        [Theory]
        [InlineData("PromtionHandlerMockRequest.json", "PromotionHandlerMockResponse.json")]
        public async Task ProductPriceHandlerTest_Check_Discount(string requestFileName, string responseFileName)
        {

            var requestData = System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "Request", requestFileName));
            _request = JsonConvert.DeserializeObject<OrderPromotionRequest>(requestData);
            var responseData = System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "Response", responseFileName));
            _response = JsonConvert.DeserializeObject<List<ProductPriceResponse>>(responseData);
            Order order = new Order();
            Promotion promotion = new Promotion();
            _promotion.GetTotalPrice(order, promotion).Returns(130);
            var handler = Substitute.For<ProductPriceHandler>(_delegatePromotionType);
            var result = await handler.Handle(_request, new CancellationToken());
            Assert.Equal(result[0].Discount, _response[0].Discount);
        }
    }
}
