using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.FunctionalTest
{
    public class TestStartupBase : Startup
    {
        public TestStartupBase(IConfiguration configuration) : base(configuration) { }
    }
}
