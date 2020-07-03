using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PromotionEngine.Business.Requests;
using PromotionEngine.Models;

namespace PromotionEngine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromotionController : ControllerBase
    {
        private readonly ILogger<PromotionController> _logger;
        private readonly IMediator _mediator;
        public PromotionController(ILogger<PromotionController> logger,IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<List<ProductPriceResponse>> Post([FromBody]OrderPromotionRequest orderPromotionRequest)
        {
            return await _mediator.Send(orderPromotionRequest); 
        }
    }
}
