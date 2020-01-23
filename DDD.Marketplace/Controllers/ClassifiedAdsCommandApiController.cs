using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Marketplace.ClassifiedAd;
using DDD.Marketplace.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static DDD.Marketplace.ClassifiedAd.Commands;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DDD.Marketplace.Controllers
{
    [Route("api/ad")]
    public class ClassifiedAdsCommandApiController : Controller
    {
        private readonly ClassifiedAdsApplicationService _applicationService;
        private readonly ILogger<ClassifiedAdsCommandApiController> _logger;

        public ClassifiedAdsCommandApiController(ClassifiedAdsApplicationService applicationService, ILogger<ClassifiedAdsCommandApiController> logger)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        public Task<IActionResult> Post(V1.Create request)
        {
            return RequestHandler.HandleCommand(request, _applicationService.Handle, _logger);
        }

        [Route("name")]
        [HttpPut]
        public Task<IActionResult> Put(V1.SetTitle request)
        {
            return RequestHandler.HandleCommand(request, _applicationService.Handle, _logger);
        }
    }
}
