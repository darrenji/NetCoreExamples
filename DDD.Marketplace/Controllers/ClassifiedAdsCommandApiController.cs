using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Marketplace.Api;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DDD.Marketplace.Controllers
{
    [Route("api/ad")]
    public class ClassifiedAdsCommandApiController : Controller
    {
        private readonly ClassifiedAdsApplicationService _applicationServce;

        public ClassifiedAdsCommandApiController(ClassifiedAdsApplicationService applicationService)
        {
            _applicationServce = applicationService;
        }
        [HttpPost]
       public async Task<IActionResult> Post(Contracts.ClassifiedAds.V1.Create request)
        {
            _applicationServce.Handle(request);
            return Ok();
        }
    }
}
