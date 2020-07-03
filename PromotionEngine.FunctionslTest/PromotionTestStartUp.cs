using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.FunctionalTest
{
    public class PromotionTestStartUp : TestStartupBase
    {

        public PromotionTestStartUp(IConfiguration configuration) : base(configuration) { }


        public class BaseRoutePattern
        {
            public string Region { get; set; }
            public string Language { get; set; }
            public string BuId { get; set; }
        }
    }
}
