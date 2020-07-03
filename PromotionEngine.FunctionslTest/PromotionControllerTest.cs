using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NSubstitute;
using NUnit.Framework;
using PromotionEngine.Business.Requests;
using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace PromotionEngine.FunctionalTest
{
    public class PromotionControllerMock
    {
        private ILogger _logger;
        private List<ProductPriceResponse> _testModel;

        public static IRequestHandler<OrderPromotionRequest, List<ProductPriceResponse>> ProductPriceMockHandler = Substitute.For<IRequestHandler<OrderPromotionRequest, List<ProductPriceResponse>>>();
        public class ProductPriceHandlerMock : IRequestHandler<OrderPromotionRequest, List<ProductPriceResponse>>
        {
            public Task<List<ProductPriceResponse>> Handle(OrderPromotionRequest request, CancellationToken cancellationToken)
            {
                return ProductPriceMockHandler.Handle(request, cancellationToken);
            }
        }


        public PromotionControllerMock()
        {
            _logger = Substitute.For<ILogger>();
            var data = System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "Response", "PromotionPostMockResponse.json"));
            _testModel = JsonConvert.DeserializeObject<List<ProductPriceResponse>>(data);
        }

        [Fact()]
        public async Task PromotionController_CheckValidReponse()
        {
            var dataRequest = System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "Request", "PromtionControllerMockRequest.json"));
            var request = JsonConvert.DeserializeObject<OrderPromotionRequest>(dataRequest);

            ProductPriceMockHandler.Handle(Arg.Any <OrderPromotionRequest>(), Arg.Any<CancellationToken>()).Returns(_testModel);
            using (var server = new TestServer(new WebHostBuilder().UseStartup<TestStartupBase>().ConfigureServices(services =>
            {
                services.AddMediatR(typeof(PromotionControllerMock));

            })))
            {
                using (var client = server.CreateClient())
                {
                    string url = $"Promotion/Post";
                    StringContent stringContent = new StringContent(
                        JsonConvert.SerializeObject(request,
                            new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }),
                        Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.PostAsync(url, stringContent);
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<ProductPriceResponse>>(responseBody);
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
            }
        }


    }//EOC
}


